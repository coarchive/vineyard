module Lexer
open Shared
open System.Text
open System.Collections.Generic

type Lexeme =
   | Let
   | BareWord of string
   | ParenLeft
   | ParenRight
   | Equals
   | Indent
   | Dedent
   | Colon
   | Caret
   | In
   | Percent
   | String of string
   | Open

type LexerState =
   | Normal
   | IndentDetect
   | String
   // | Char
   // | Escaped

let lex (input: string array): Lexeme List =
   let out = List<Lexeme>()

   let state  = Stack<LexerState>()
   let levels = Stack<int>()
   let mutable currentLevel = 0

   let mutable i = 0

   state.Push Normal
   levels.Push -1

   while i < input.Length do
      match state.Peek() with
      | Normal ->
         let token = input.[i]
         match token with
         | "let"  -> out.Add Let
         | " "    -> ()
         | "("    -> out.Add ParenLeft
         | ")"    -> out.Add ParenRight
         | "="    -> out.Add Equals
         | "\n"   -> state.Push IndentDetect
         | ":"    -> out.Add Colon
         | "^"    -> out.Add Caret
         | "in"   -> out.Add In
         | "%"    -> out.Add Percent
         | "\""   -> state.Push String
         | "open" -> out.Add Open
         | word   -> out.Add ^| BareWord word
         i <- i + 1
      | IndentDetect ->
         let mutable spaceCount = 0
         while input.[i] = " " do
            i <- i + 1
            spaceCount <- spaceCount + 1
         if input.[i] <> "\n" then
            // [ ][ ][let]...
            //       i
            // currently, i rests on a non-space
            let lastLevel = levels.Peek()
            if spaceCount <= lastLevel then
               levels.Pop() |> ignore
               out.Add Dedent
            elif spaceCount > currentLevel then
               levels.Push currentLevel
               currentLevel <- spaceCount
               out.Add Indent
         state.Pop() |> ignore
      | String ->
         let workingString = StringBuilder()
         while input.[i] <> "\"" do
            workingString.Append input.[i] |> ignore
            i <- i + 1
         // currently, i rests on the end quote
         out.Add ^| Lexeme.String ^| workingString.ToString()
         i <- i + 1
         state.Pop() |> ignore
   out
