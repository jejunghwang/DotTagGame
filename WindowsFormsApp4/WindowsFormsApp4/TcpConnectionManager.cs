using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApp4
{
    public class TcpConnectionManager
    {
        private TcpClient client;
        private CancellationTokenSource cts;

        public NetworkStream Stream => client?.GetStream();
        public bool IsConnected => client?.Connected ?? false;
        public event Action<byte[]> PacketReceived;

        public async Task ConnectAsync(string ip, int port)
        {
            if (client != null && client.Connected)
                return;

            client = new TcpClient();
            await client.ConnectAsync(ip, port).ConfigureAwait(false);

            cts?.Cancel();
            cts = new CancellationTokenSource();

            _ = ReadLoopAsync(cts.Token);
        }

        private async Task ReadLoopAsync(CancellationToken token)
        {
            try
            {
                var stream = Stream;
                while (!token.IsCancellationRequested && client.Connected)
                {
                    Debug.WriteLine("[Client]: Awaiting header...");

                    var header = new byte[4];
                    int read = await stream.ReadAsync(header, 0, header.Length, token).ConfigureAwait(false);
                    if (read == 0) break;

                    int bodyLen = BitConverter.ToInt32(header, 0);
                    Debug.WriteLine($"[Client]: Header received, bodyLen={bodyLen}");

                    var body = new byte[bodyLen];
                    int offset = 0;
                    while (offset < bodyLen)
                    {
                        int chunk = await stream.ReadAsync(body, offset, bodyLen - offset, token).ConfigureAwait(false);
                        if (chunk == 0) throw new SocketException();
                        offset += chunk;
                    }

                    PacketReceived?.Invoke(body);
                }
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("[Client]: Read loop canceled.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Client] ReadLoop error: {ex.Message}");
            }
            finally
            {
                Debug.WriteLine("[Client]: Disconnected.");
                Close();
            }
        }
        public void Close()
        {
            cts?.Cancel();
            try { Stream?.Close(); } catch { }
            try { client?.Close(); } catch { }
        }
    }
}
