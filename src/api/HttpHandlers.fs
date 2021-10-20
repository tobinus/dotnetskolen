namespace NRK.Dotnetskolen.Api

module HttpHandlers =

    open Microsoft.AspNetCore.Http
    open Giraffe

    let epgHandler (dateAsString: string) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) -> (json dateAsString) next ctx
