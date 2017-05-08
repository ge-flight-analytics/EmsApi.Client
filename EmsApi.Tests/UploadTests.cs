using System;
using System.IO;
using Xunit;
using FluentAssertions;

using EmsApi.Dto.V2;

namespace EmsApi.Tests
{
    public class UploadTests : TestBase
    {
        [Fact( DisplayName = "Upload file should transfer file content" )]
        public void Upload_file_should_transfer_file_content()
        {
            using( var api = NewService() )
            using( var tempFile = new TempFile() )
            {
                DateTime start = DateTime.UtcNow;
                string content = "Test file upload...";
                File.WriteAllText( tempFile.Path, content, System.Text.Encoding.UTF8 );

                var request = new UploadRequest();
                request.Name = "Test upload";
                request.Type = UploadRequestType.TestTransfer;
                UploadRecord result = api.Transfers.UploadFile( tempFile.Path, request, emsSystem: 1 );
                result.TransferFinishTime.Should().NotBeNull();
                result.TransferFinishTime.Should().BeAfter( start );
            }
        }

        [Fact( DisplayName = "Upload stream should transfer stream content" )]
        public void Upload_stream_should_transfer_stream_content()
        {

        }

        private class TempFile : IDisposable
        {
            public TempFile()
            {
                Path = System.IO.Path.GetTempFileName();
            }

            public string Path { get; set; }

            public void Dispose()
            {
                if( !string.IsNullOrEmpty( Path ) && File.Exists( Path ) )
                    File.Delete( Path );
            }
        }
    }
}
