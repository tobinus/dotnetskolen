namespace NRK.Dotnetskolen.Api

module HttpHandlers =

    open Microsoft.AspNetCore.Http
    open Giraffe
    open System
    open System.Globalization
    open System.Threading.Tasks

    let parseAsDateTime (dateAsString: string) : DateTime option =
        try
            let date =
                DateTime.ParseExact(dateAsString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None)

            Some date
        with
        | _ -> None

    let epgHandler (dateAsString: string) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            match (parseAsDateTime dateAsString) with
            | Some date -> (json date) next ctx
            | None -> RequestErrors.badRequest (text "Invalid date") (Some >> Task.FromResult) ctx
