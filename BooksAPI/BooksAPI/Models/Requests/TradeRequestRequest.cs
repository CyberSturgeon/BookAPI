using System.ComponentModel;

namespace SampleBackend.Models.Requests;

public class TradeRequestRequest
{
    public Guid BookId { get; set; }

    public Guid UserId { get; set; }

    //[DefaultValue(Status.Waiting)]
    public string TradeStatus { get; set; }
}

//public enum Status
//{
//    Waiting,
//    Accepted,
//    Declined
//}
