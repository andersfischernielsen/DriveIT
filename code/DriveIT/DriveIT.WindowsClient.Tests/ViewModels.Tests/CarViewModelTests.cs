using System;
using DriveIT.WindowsClient.ViewModels;
using NUnit.Framework;

namespace DriveIT.WindowsClient.Tests.ViewModels.Tests
{
    [TestFixture]
    public class CarViewModelTests
    {

        #region ImageGalleryTests
        [Test]
        public void NextPictureWithOnePicture()
        {
            var carViewModel = new CarViewModel();
            carViewModel.SelectedImageViewModel.ImagePath = "image1";

            Assert.AreEqual(1, carViewModel.ImageGallery.Count);
            Assert.AreEqual("image1", carViewModel.SelectedImageViewModel.ImagePath);

            carViewModel.NextImage();
            Assert.AreEqual("image1", carViewModel.SelectedImageViewModel.ImagePath);
        }

        [Test]
        public void NextPictureWithTwoPicture()
        {
            var carViewModel = new CarViewModel();
            carViewModel.SelectedImageViewModel.ImagePath = "image1";
            carViewModel.AddImage();
            carViewModel.SelectedImageViewModel.ImagePath = "image2";

            Assert.AreEqual(2, carViewModel.ImageGallery.Count);
            Assert.AreEqual("image2", carViewModel.SelectedImageViewModel.ImagePath);

            carViewModel.NextImage();
            Assert.AreEqual("image1", carViewModel.SelectedImageViewModel.ImagePath);

            carViewModel.NextImage();
            Assert.AreEqual("image2", carViewModel.SelectedImageViewModel.ImagePath);
        }

        [Test]
        public void NextPictureWithThreePicture()
        {
            var carViewModel = new CarViewModel();
            carViewModel.SelectedImageViewModel.ImagePath = "image1";
            carViewModel.AddImage();
            carViewModel.SelectedImageViewModel.ImagePath = "image2";
            carViewModel.AddImage();
            carViewModel.SelectedImageViewModel.ImagePath = "image3";

            Assert.AreEqual(3, carViewModel.ImageGallery.Count);
            Assert.AreEqual("image3", carViewModel.SelectedImageViewModel.ImagePath);

            carViewModel.NextImage();
            Assert.AreEqual("image1", carViewModel.SelectedImageViewModel.ImagePath);

            carViewModel.NextImage();
            Assert.AreEqual("image2", carViewModel.SelectedImageViewModel.ImagePath);

            carViewModel.NextImage();
            Assert.AreEqual("image3", carViewModel.SelectedImageViewModel.ImagePath);
        }

        [Test]
        public void PreviousPictureWithOnePicture()
        {
            var carViewModel = new CarViewModel();
            carViewModel.SelectedImageViewModel.ImagePath = "image1";

            Assert.AreEqual(1, carViewModel.ImageGallery.Count);
            Assert.AreEqual("image1", carViewModel.SelectedImageViewModel.ImagePath);

            carViewModel.PreviousImage();
            Assert.AreEqual("image1", carViewModel.SelectedImageViewModel.ImagePath);
        }

        [Test]
        public void PreviousPictureWithTwoPicture()
        {
            var carViewModel = new CarViewModel();
            carViewModel.SelectedImageViewModel.ImagePath = "image1";
            carViewModel.AddImage();
            carViewModel.SelectedImageViewModel.ImagePath = "image2";

            Assert.AreEqual(2, carViewModel.ImageGallery.Count);
            Assert.AreEqual("image2", carViewModel.SelectedImageViewModel.ImagePath);

            carViewModel.PreviousImage();
            Assert.AreEqual("image1", carViewModel.SelectedImageViewModel.ImagePath);

            carViewModel.PreviousImage();
            Assert.AreEqual("image2", carViewModel.SelectedImageViewModel.ImagePath);
        }

        [Test]
        public void PreviousPictureWithThreePicture()
        {
            var carViewModel = new CarViewModel();
            carViewModel.SelectedImageViewModel.ImagePath = "image1";
            carViewModel.AddImage();
            carViewModel.SelectedImageViewModel.ImagePath = "image2";
            carViewModel.AddImage();
            carViewModel.SelectedImageViewModel.ImagePath = "image3";

            Assert.AreEqual(3, carViewModel.ImageGallery.Count);
            Assert.AreEqual("image3", carViewModel.SelectedImageViewModel.ImagePath);

            carViewModel.PreviousImage();
            Assert.AreEqual("image2", carViewModel.SelectedImageViewModel.ImagePath);

            carViewModel.PreviousImage();
            Assert.AreEqual("image1", carViewModel.SelectedImageViewModel.ImagePath);

            carViewModel.PreviousImage();
            Assert.AreEqual("image3", carViewModel.SelectedImageViewModel.ImagePath);
        }

        [Test]
        public void DeleteOnlyPicture()
        {
            var carViewModel = new CarViewModel();
            carViewModel.SelectedImageViewModel.ImagePath = "image1";
            Assert.AreEqual(1, carViewModel.ImageGallery.Count);
            carViewModel.DeleteImage();
            Assert.AreEqual(1, carViewModel.ImageGallery.Count);
            Assert.AreEqual(null, carViewModel.ImageGallery[0].ImagePath);
            Assert.AreEqual(null, carViewModel.SelectedImageViewModel.ImagePath);
        }

        [Test]
        public void DeleteOneOfTwoPictures()
        {
            var carViewModel = new CarViewModel();
            carViewModel.SelectedImageViewModel.ImagePath = "image1";
            carViewModel.AddImage();
            carViewModel.SelectedImageViewModel.ImagePath = "image2";
            Assert.AreEqual(2, carViewModel.ImageGallery.Count);
            carViewModel.DeleteImage();
            Assert.AreEqual(1, carViewModel.ImageGallery.Count);
            Assert.AreEqual("image1", carViewModel.ImageGallery[0].ImagePath);
            Assert.AreEqual("image1", carViewModel.SelectedImageViewModel.ImagePath);

            carViewModel = new CarViewModel();
            carViewModel.SelectedImageViewModel.ImagePath = "image1";
            carViewModel.AddImage();
            carViewModel.SelectedImageViewModel.ImagePath = "image2";
            carViewModel.SelectedImageViewModel = carViewModel.ImageGallery[0];
            Assert.AreEqual(2, carViewModel.ImageGallery.Count);
            carViewModel.DeleteImage();
            Assert.AreEqual(1, carViewModel.ImageGallery.Count);
            Assert.AreEqual("image2", carViewModel.ImageGallery[0].ImagePath);
            Assert.AreEqual("image2", carViewModel.SelectedImageViewModel.ImagePath);
        }

        [Test]
        public void AddPictures()
        {
            var carViewModel = new CarViewModel();
            carViewModel.SelectedImageViewModel.ImagePath = "image1";
            Assert.AreEqual(1, carViewModel.ImageGallery.Count);
            Assert.AreEqual("image1", carViewModel.ImageGallery[0].ImagePath);

            carViewModel.AddImage();
            carViewModel.SelectedImageViewModel.ImagePath = "image2";
            Assert.AreEqual(2, carViewModel.ImageGallery.Count);

            Assert.AreEqual("image1", carViewModel.ImageGallery[0].ImagePath);
            Assert.AreEqual("image2", carViewModel.ImageGallery[1].ImagePath);

            carViewModel.AddImage();
            Assert.AreEqual(3, carViewModel.ImageGallery.Count);
            Assert.AreEqual(null, carViewModel.ImageGallery[2].ImagePath);
        }

        #endregion ImageGalleryTests

    }
}
