module Parser
open Holo

[<Struct>]
type Binding = {Name: string; Holo: Holo}

[<Struct>]
type FunctionCall = {Name: string; Args: }

type Statement =
   | Binding of Binding
   | 

type FunctionBody =
   | Extern
   | Statement of Statement array

[<Struct>]
type Function = {
   Name: string;
   Params: Binding array;
   Returns: Holo;
   Body: FunctionBody;
}


type ParserState =
   | TopLevel
   | Open
   | FunctionDeclaration
   | FunctionParams
   | FunctionReturn
   | CodeBlock
   | Binding
   | BindingType
