using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Microsoft.Playwright;

namespace copibara.tests;

[Binding]
public class StepDefinitions
{
    private IPlaywright _playwright;
    private IBrowser browser;
    private IPage _page;
    private readonly ScenarioContext _scenarioContext;

    public StepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        await browser.CloseAsync();
        _playwright.Dispose();
    }

    [Given(@"I have a todo list")]
    public async Task GivenIhaveatodolist()
    {
        _playwright = await Playwright.CreateAsync();
        browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });
        _page = await browser.NewPageAsync();
        await _page.GotoAsync("https://demo.playwright.dev/todomvc/");

        await _page.GetByPlaceholder("What needs to be done?").ClickAsync();
    }


    [When(@"I open the todo page")]
    public async Task WhenIopenthetodopage()
    {
        await _page.GetByPlaceholder("What needs to be done?").ClickAsync();
    }


    [When(@"I add a todo")]
    public async Task WhenIaddatodo(Table table)
    {
        var entries = table.CreateSet<TodoItem>();

        foreach (var entry in entries)
        {
            await _page.GetByPlaceholder("What needs to be done?").FillAsync(entry.Text);

            await _page.GetByPlaceholder("What needs to be done?").PressAsync("Enter");
        }
    }


    [When(@"I remove the todo")]
    public async Task WhenIremovethetodo(Table table)
    {
        var entries = table.CreateSet<TodoItem>();

        foreach (var entry in entries)
        {
            await _page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = entry.Text })
                .GetByRole(AriaRole.Checkbox, new() { Name = "Toggle Todo" })
                .CheckAsync();
        }
    }


    [Then(@"I should see the todo in the list")]
    public async Task ThenIshouldseethetodointhelist(Table table)
    {

        var entries = table.CreateSet<TodoItem>();

        foreach (var entry in entries)
        {
            var isVisible = await _page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = entry.Text }).IsVisibleAsync();
            Assert.That(isVisible, Is.True);
        }
    }

    [Then(@"I should see the todo list")]
    public async Task ThenIshouldseethetodolist()
    {
        var isVisible = await _page.GetByPlaceholder("What needs to be done?").IsVisibleAsync();
        Assert.That(isVisible, Is.True);
    }


    [When(@"I complete the todo")]
    public async Task WhenIcompletethetodo(Table table)
    {
        var entries = table.CreateSet<TodoItem>();

        foreach (var entry in entries)
        {
            await _page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = entry.Text })
                .GetByRole(AriaRole.Checkbox, new() { Name = "Toggle Todo" })
                .CheckAsync();
        }
    }


    [Then(@"I should see the completed todo in the list")]
    public async Task ThenIshouldseethecompletedtodointhelist(Table table)
    {
        var entries = table.CreateSet<TodoItem>();

        foreach (var entry in entries)
        {
            var isVisible = await _page.GetByText(entry.Text).IsVisibleAsync();
            Assert.That(isVisible, Is.True);
        }
    }

}

public record TodoItem(string Text);