﻿namespace BookSwapping.Models.InputModels.Book
{
    public class EditBookInputViewModel : CreateBookInputModel
    {
        public byte[] ExistingPhotoPath { get; set; }
    }
}
