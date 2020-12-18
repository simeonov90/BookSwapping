namespace BookSwapping.Models.InputModels.Book
{
    using BookSwapping.Common;
    using BookSwapping.Data.Models;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateBookInputModel
    {
        [Required(ErrorMessage = ErrorMesseges.RequiredField)]
        [Display(Name = "Заглавие")]
        public string BookName { get; set; }

        [Required(ErrorMessage = ErrorMesseges.RequiredField)]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Required(ErrorMessage = ErrorMesseges.RequiredField)]
        [Display(Name = "Снимка")]
        public IFormFile FormFile { get; set; }

        [Required(ErrorMessage = ErrorMesseges.RequiredField)]
        [Display(Name = "Жанр")]
        public string TypeGenre { get; set; }
        public List<string> Genre { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

    }
}
