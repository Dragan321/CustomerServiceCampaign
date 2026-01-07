using System.Xml.Serialization;

namespace CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Customers.Models;

[XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
public class SoapEnvelope<T>
{
    [XmlElement(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public object Header { get; set; }

    [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public SoapBody<T> Body { get; set; }
}

public class SoapBody<T>
{
    [XmlElement(ElementName = "FindPerson", Namespace = "http://tempuri.org")]
    public T FindPerson { get; set; }
    
    [XmlElement(ElementName = "FindPersonResponse", Namespace = "http://tempuri.org")]
    public T FindPersonResponse { get; set; }
}

[XmlType(Namespace = "http://tempuri.org")]
public class FindPersonRequest
{
    [XmlElement(ElementName = "id")]
    public int Id { get; set; }
}

[XmlType(Namespace = "http://tempuri.org")]
public class FindPersonResponse
{
    [XmlElement(ElementName = "FindPersonResult")]
    public FindPersonResult FindPersonResult { get; set; }
}

public class FindPersonResult
{
    [XmlElement(ElementName = "Name", Namespace = "http://tempuri.org")]
    public string Name { get; set; }

    [XmlElement(ElementName = "SSN", Namespace = "http://tempuri.org")]
    public string Ssn { get; set; }

    [XmlElement(ElementName = "DOB", Namespace = "http://tempuri.org")]
    public string Dob { get; set; }

    [XmlElement(ElementName = "Age", Namespace = "http://tempuri.org")]
    public int Age { get; set; }
}
