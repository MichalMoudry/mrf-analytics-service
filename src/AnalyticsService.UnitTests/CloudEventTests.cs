using System.Text.Json;
using Dapr;

namespace AnalyticsService.UnitTests;

internal sealed class CloudEventTests
{
    [Test]
    public void TestDlqCloudEvent()
    {
        var poisonedMessage = """"
{
    "data": {
        "test_val_1": 5432,
        "test_val_2": "string_val",
        "test_val_3": true
    },
    "datacontenttype": "application/json",
    "id": "e2e0f099-6b7f-4a00-a549-bd5a4366a66d",
    "pubsubname": "pub-sub",
    "source": "doc-service",
    "specversion": "1.0",
    "time": "2023-11-16T12:49:53Z",
    "topic": "statistic-poison",
    "traceid": "00-f9a224367f5cd4694210021220bc9a68-c74f791fd741165b-01",
    "traceparent": "00-f9a224367f5cd4694210021220bc9a68-c74f791fd741165b-01",
    "tracestate": "",
    "type": "com.dapr.event.sent"
}
"""";
        var cloudEvent = JsonSerializer.Deserialize<CloudEvent>(poisonedMessage);
    }
}