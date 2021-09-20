// Defining messages
type DTGCafeMessage = | OrderDrink of Drink * int  // Drink and qty
                      | LeaveAComment of string

// Defining mailbox processor agent
type Agent<'DTGCafeMessage> = MailboxProcessor<'DTGCafeMessage>

// Defining drink to go cafe drink agent function
let cafeAgent =
    new Agent<_> (fun inbox ->
        let rec cafeHandler=
            async {
                let! msg = inbox.Receive()
                match msg with
                |OrderDrink (drink,quantity) -> getPriceWithMessage drink quantity
                |LeaveAComment s -> printfn "%s" (commentHandler s)
                return! cafeHandler
            }
        cafeHandler)