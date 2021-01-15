using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CourierBA.Helpers
{
    public class MediaTask
    {
        private IMedia _media;

        public string FileName { get; set; }
        public string FolderName { get; set; }

        public MediaTask()
        {
            FileName = "file" + DateTime.Now.Ticks + ".jpg";
            FolderName = "Photos";
        }

        public MediaTask(string filename)
        {
            FileName = 
                string.IsNullOrEmpty(filename) 
                ? "file" + "file" + DateTime.Now.Ticks + ".jpg" 
                : filename;
            FolderName = "Photos";
        }

        public async Task<PhotoResult> TakePhoto(StoreCameraMediaOptions options = null)
        {
            _media = CrossMedia.Current;
            if (await _media.Initialize())
            {
                if (!_media.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    return new PhotoResult { Photo = null, Succes = false, Messege = "No tienes camara", Sender = null};
                }
                if (options == null)
                {
                    options = new StoreCameraMediaOptions
                    {
                        Directory = FolderName,
                        Name = FileName,
                        CompressionQuality = 25
                    };
                }
                try
                {
                    MediaFile file = await _media.TakePhotoAsync(options);
                    if (file != null)
                    {
                        return new PhotoResult { Photo = file, Succes = true, Messege = "Se tomo la foto" };

                    }
                }
                catch (TaskCanceledException)
                {
                    ; return new PhotoResult { Photo = null, Succes = false, Messege = "Ha ocurriso algun error", Sender = null };
                }
            }
            return new PhotoResult { Photo = null, Succes = false, Messege = "Ha ocurriso algun error", Sender = null };
        }

        public class PhotoResult
        {
            public string Messege { get; internal set; }
            public MediaFile Photo { get; internal set; }
            public bool Succes { get; internal set; }
            public MediaTask Sender { get; set; }

        }

    }
}
