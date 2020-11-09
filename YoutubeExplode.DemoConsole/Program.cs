using System;
using System.Threading.Tasks;
using YoutubeExplode.DemoConsole.Internal;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace YoutubeExplode.DemoConsole {
    public static class Program {
        public static async Task<int> Main() {
            Console.Title = "YoutubeExplode Demo";

            // This demo prompts for video ID and downloads one media stream
            // It's intended to be very simple and straight to the point
            // For a more complicated example - check out the WPF demo

            var youtube = new YoutubeClient();

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length <= 1) {
                return 0;
            }
            Console.Write("Video Id: " + args[1]);
            // Read the video ID
            var videoId = new VideoId(args[1]);

            // Get media streams & choose the best muxed stream
            var streams = await youtube.Videos.Streams.GetManifestAsync(videoId);
            var streamInfo = streams.GetAudioOnly().WithHighestBitrate();
            if (streamInfo == null) {
                Console.Error.WriteLine("This videos has no streams");
                return -1;
            }

            // Compose file name, based on metadata
            var fileName = args[2];

            // Download video
            Console.Write($"Downloading stream: {streamInfo.Bitrate} / {streamInfo.Container.Name}... ");
            using (var progress = new InlineProgress())
                await youtube.Videos.Streams.DownloadAsync(streamInfo, fileName, progress);

            Console.WriteLine($"Video saved to '{fileName}'");
            return 0;
        }
    }
}
