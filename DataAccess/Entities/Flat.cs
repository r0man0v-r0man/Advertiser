using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Flat
    {
        public int Id { get; set; }
        /// <summary>
        /// Количество комнат
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
        /// <summary>
        /// Фотография объявления
        /// </summary>
        public string Image { get; set; }
    }
}
