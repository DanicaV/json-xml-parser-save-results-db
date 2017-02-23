using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JsonXmlParserSaveResultsToDB.Database
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string Value { get; set; }
        public virtual List<Popup> Popups { get; set; }

        public Menu()
        {
            this.Popups = new List<Popup>();
        }
    }

    public class Popup
    {
        public int PopupId { get; set; }
        public string Value { get; set; }
        public string OnClick { get; set; }
        public virtual Menu MenuA { get; set; }
    }
}
