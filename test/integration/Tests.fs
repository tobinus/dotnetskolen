module Tests

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
