open System.IO

let helloWorld = __SOURCE_DIRECTORY__ + "/../examples/hello_world.vyd"

let tokens =
   helloWorld
   |> File.ReadAllText
   |> Tokenizer.tokenize

for token in tokens do
   if token = "\n" then
      printf "[lf]"
   else
      printf "[%s]" token

printfn ""

let lexemes =
   tokens.ToArray()
   |> Lexer.lex

for lexeme in lexemes do
   printf "[%s]" (lexeme.ToString())
