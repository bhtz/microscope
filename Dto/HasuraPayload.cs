using System;

namespace Microscope.dto
{
    public class HasuraPayload {
        public Payload payload { get; set; }
    }
    
    public class Payload
    {
        public string id { get; set; }
        public Event @event { get; set; }
        public DateTime created_at { get; set; }
        public DeliveryInfo delivery_info { get; set; }
        public Trigger trigger { get; set; }
        public Table table { get; set; }
    }

    public class Event
    {
        public dynamic session_variables { get; set; }
        public string op { get; set; }
        public Data data { get; set; }
    }

    public class Data 
    {
        public dynamic @new {get; set;}
        public dynamic old {get; set;}
    }

    public class DeliveryInfo
    {
        public int max_retries { get; set; }
        public int current_retry { get; set; }
    }

    public class Trigger
    {
        public string name { get; set; }
    }

    public class Table
    {
        public string schema { get; set; }
        public string name { get; set; }
    }
}
