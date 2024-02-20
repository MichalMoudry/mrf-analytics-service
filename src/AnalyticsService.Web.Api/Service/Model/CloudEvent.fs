namespace AnalyticsService.Web.Api.Service.Model

/// A record encapsulating a cloud event.
type CloudEvent<'T> = {
    Source: string
    Topic: string
    Data: 'T
}