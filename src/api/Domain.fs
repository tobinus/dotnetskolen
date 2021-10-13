namespace NRK.Dotnetskolen

module Domain =

    open System
    open System.Text.RegularExpressions

    type Sending =
        { Tittel: string
          Kanal: string
          StartTidspunkt: DateTimeOffset
          SluttTidspunkt: DateTimeOffset }

    type Epg = Sending list

    let isTitleValid (title: string) : bool =
        let titleRegex = Regex(@"^[\p{L}0-9\.,-:!]{5,100}$")
        titleRegex.IsMatch(title)

    let isChannelValid (channel: string) : bool = channel = "NRK1" || channel = "NRK2"

    let areStartAndEndTimesValid (startTime: DateTimeOffset) (endTime: DateTimeOffset) = startTime < endTime

    let isTransmissionValid (transmission: Sending) =
        isTitleValid transmission.Tittel
        && isChannelValid transmission.Kanal
        && areStartAndEndTimesValid transmission.StartTidspunkt transmission.SluttTidspunkt
