using System;
using System.Collections.Generic;
using System.Text;

namespace Gadget.Core.Tools
{
    public class TestTool : IGadgetTool<GadgetData>
    {
        public string Go(GadgetData input)
        {
            return "<img src='https://upload.wikimedia.org/wikipedia/en/thumb/4/4c/Inspector_Gadget_DIC_animated_series_title_card.png/250px-Inspector_Gadget_DIC_animated_series_title_card.png'/>";
        }
    }
}
