using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WandelApp.Views
{

    public class NewMainPageMenuItem
    {
        public NewMainPageMenuItem()
        {
            TargetType = typeof(NewMainPageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}