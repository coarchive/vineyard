module Lexer
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

type LexerState =
   | Normal
   | IndentDetect
   | String
   // | Char
   // | Escaped

let lex (input: string array): Lexeme List =
   let out = List<Lexeme>()

   let state  = Stack<LexerState>()
   let levels = Stack<uint>()

   let mutable i = 0

   state.Push Normal
   levels.Push 0u

   while i < input.Length do
      match state.Peek() with
      | Normal ->
         let token = input.[i]
         match token with
         | "let" -> out.Add Let
         | " "   -> ()
         | "("   -> out.Add ParenLeft
         | ")"   -> out.Add ParenRight
         | "="   -> out.Add Equals
         | "\n"  -> state.Push IndentDetect
         | ":"   -> out.Add Colon
         | "^"   -> out.Add Caret
         | "in"  -> out.Add In
         | "%"   -> out.Add Percent
         | "\""  -> state.Push String
         | word  -> out.Add (BareWord word)
         i <- i + 1
      | IndentDetect ->
         let mutable spaceCount = 0u
         while input.[i] = " " do
            i <- i + 1
            spaceCount <- spaceCount + 1u
         if input.[i] <> "\n" then
         // [ ][ ][let]...
         //       i
         // currently, i rests on a non-space
            let lastLevel = levels.Peek()
            if spaceCount > lastLevel then
               levels.Push(spaceCount)
               out.Add(Indent)
            elif spaceCount < lastLevel then
               levels.Pop() |> ignore
               out.Add(Dedent)
         state.Pop() |> ignore
      | String ->
         let workingString = StringBuilder()
         while input.[i] <> "\"" do
            workingString.Append input.[i] |> ignore
            i <- i + 1
         // currently, i rests on the end quote
         out.Add(Lexeme.String (workingString.ToString()))
         i <- i + 1
         state.Pop() |> ignore
   out
