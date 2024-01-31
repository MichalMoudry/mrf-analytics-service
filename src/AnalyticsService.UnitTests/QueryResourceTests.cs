using AnalyticsService.Database.Queries;

namespace AnalyticsService.UnitTests;

internal sealed class QueryResourceTests
{
    [Test]
    public void TestQueryWhitespaces()
    {
        string[] queries = [
            Query.GetDlqItems,
            Query.GetBatchStats,
            Query.InsertBatchStat,
            Query.InsertDlqTopic
        ];
        Assert.Multiple(() =>
        {
            foreach (var query in queries)
            {
                Assert.That(query, Is.Not.Null.Or.Empty);
            }
        });
    }
}