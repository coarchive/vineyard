module Tokenizer
open System.Collections.Generic
open System.Text

let tokenize (input: string): string List =
   let out = List<string>()
   let bareWord = StringBuilder()
   if not (input.EndsWith "\n") then
      failwith "Input must end with newline!"

   for c in input do
      match c with
      | ' '
      | '('
      | ')'
      | '='
      | '\n'
      | ':'
      | '^'
      | '%'
      | '='
      | '"'
      | ','
      | '\''
      | '$' ->
         if bareWord.Length > 0 then
            out.Add(bareWord.ToString()) |> ignore
            bareWord.Clear() |> ignore
         out.Add(c.ToString())
      | _ ->
         bareWord.Append(c.ToString()) |> ignore
   out
