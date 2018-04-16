using System;
using System.Collections.Generic;
using LeetPhotos.Core.Models;

namespace LeetPhotos.Core.Services
{
    public interface IPhotoService
    {
        List<Photo> GetPhotos();
    }
}
