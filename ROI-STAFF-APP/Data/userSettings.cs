using System;
using SQLite;

namespace ROI_STAFF_APP.Data
{
	public class UserSettingsPage
	{
        public class UserSet
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string Name { get; set; }
            public int FontSize { get; set; }
            public bool lightOrDark { get; set; }
        }
    }
}

