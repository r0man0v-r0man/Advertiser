using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.FlatModels
{
    public class FlatModel : Model
    {
        /// <summary>
        /// кол-во комнат
        /// </summary>
        public int Rooms { get; set; }
        /// <summary>
        /// Активно или нет 
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Цена 
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Описание квартиры
        /// </summary>
        public string Description { get; set; }
    }
}
