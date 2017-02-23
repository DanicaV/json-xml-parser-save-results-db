using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonXmlParserSaveResultsToDB.Database
{
    class MenuContext : DbContext
    {
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Popup> Popup { get; set; }
    }
}
