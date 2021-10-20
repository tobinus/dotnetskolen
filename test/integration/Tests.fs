module Tests

open System
open System.Net
open System.IO
open Microsoft.AspNetCore.TestHost
open Microsoft.AspNetCore.Hosting
open Xunit
open NRK.Dotnetskolen.Api

let createWebHostBuilder () =
    WebHostBuilder()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseEnvironment("Test")
        .Configure(Program.configureApp)
        .ConfigureServices(Program.configureServices)

[<Fact>]
let ``Get ping returns 200 OK`` () =
    async {
        use testServer = new TestServer(createWebHostBuilder ())
        use client = testServer.CreateClient()
        let url = "/ping"

        let! response = client.GetAsync(url) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore
    }

[<Fact>]
let ``Get EPG today returns 200 OK`` () =
    async {
        use testServer = new TestServer(createWebHostBuilder ())
        use client = testServer.CreateClient()
        let todayAsString = DateTimeOffset.Now.ToString "yyyy-MM-dd"
        let url = sprintf "/epg/%s" todayAsString

        let! response = client.GetAsync(url) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore
    }

[<Fact>]
let ``Get EPG invalid date returns bad request`` () =
    async {
        use testServer = new TestServer(createWebHostBuilder ())
        use client = testServer.CreateClient()
        let invalidDateAsString = "2021-13-32"
        let url = sprintf "/epg/%s" invalidDateAsString

        let! response = client.GetAsync(url) |> Async.AwaitTask

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode)
    }
