using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace minicalendar.Client.Configuration;

public static class RenderModes {
    public static IComponentRenderMode InteractiveWebAssemblyWithoutPrerendering { get; } = new InteractiveWebAssemblyRenderMode(prerender: false);

    public static InteractiveWebAssemblyRenderMode DefaultRenderMode { get; } = new(prerender: false);
}