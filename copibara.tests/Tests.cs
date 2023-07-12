namespace copibara.tests;

using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [Test]
    public async Task MyTest()
    {
        await Page.GotoAsync("https://demo.playwright.dev/todomvc/");

        await Page.GotoAsync("https://demo.playwright.dev/todomvc/#/");

        await Page.GetByPlaceholder("What needs to be done?").ClickAsync();

        await Page.GetByPlaceholder("What needs to be done?").FillAsync("Something stupid");

        await Page.GetByPlaceholder("What needs to be done?").PressAsync("Enter");

        await Page.GetByPlaceholder("What needs to be done?").FillAsync("Hell no");

        await Page.GetByPlaceholder("What needs to be done?").PressAsync("Enter");

        await Page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "Hell no" }).GetByRole(AriaRole.Checkbox, new() { Name = "Toggle Todo" }).CheckAsync();

        await Page.GetByRole(AriaRole.Button, new() { Name = "Delete" }).ClickAsync();

    }
}
