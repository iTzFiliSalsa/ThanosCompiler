using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace frmMain
{
    class AleGrammar : Grammarr
    {

        RegexLexer csLexer = new RegexLexer();
        bool load;
        List<string> palabrasReservadas;

        private MainForm mainForm;

        public AleGrammar(MainForm mainForm)
        {
            initStartRules();
            initProductionRules();
            initPOS();

            this.mainForm = mainForm;
        }

        public override void initStartRules()
        {
            var rules = new List<string>()
            {

                "<class> class <id> { }",
                "<class> class <id> { <statement-list> }",
                "<class> class <id> { <statement-list> <class> static void main ( String []args ) { } }",
                "<class> class <id> { <class> static void main ( String []args ) { <statement-list> } }",
                "<class> class <id> { <class> static void main ( String []args ) { } }",
                "<class> class <id> { <statement-list> <class> static void main ( String []args ) { <statement-list> } }",
                "<class> class <id> { <class> static void main ( String []args ) { } <statement-list> }",
                "<class> class <id> { <statement-list> <class> static void main ( String []args ) { <statement-list> } <statement-list> }",
                "<class> class <id> { <statement-list> <class> static void main ( String []args ) { } <statement-list> }",
                "<class> class <id> { <statement-list> <class> static void main ( String []args ) { <statement-list> } }",
                "<class> class <id> { <class> static void main ( String []args ) { <statement-list> do { } while ( <condition> ) ; } }",
                "<class> class <id> { <class> static void main ( String []args ) { <statement-list> do { <statement-list> } while ( <condition> ) ; } }",
                "<class> class <id> { <class> static void main ( String []args ) { <statement-list> switch ( <id> ) { case <number> : <statement-list> break ; } } }",
                "<class> class <id> { <class> static void main ( String []args ) { <statement-list> if ( <condition> ) { } } }",
                "<class> class <id> { <class> static void main ( String []args ) { <statement-list> if ( <condition> ) { } else { } } }",
                "<class> class <id> { <class> static void main ( String []args ) { if ( <condition> ) { } } }",
                "<class> class <id> { <class> static void main ( String []args ) { if ( <condition> ) { } <statement-list> } }",
                "<class> class <id> { <class> static void main ( String []args ) { <statement-list> if ( <condition> ) { } <statement-list> } }",
                "<class> class <id> { <class> static void main ( String []args ) { if ( <condition> ) { } else { } } }",
                "<class> class <id> { <class> static void main ( String []args ) { if ( <condition> ) { } else { <statement-list> } } }",
                "<class> class <id> { <class> static void main ( String []args ) { if ( <condition> ) { } else { <statement-list> } <statement-list> } }",
                "<class> class <id> { <class> static void main ( String []args ) { <statement-list> if ( <condition> ) { } else { <statement-list> } <statement-list> } }",
                "<class> class <id> { <class> static void main ( String []args ) { <statement-list> if ( <condition> ) { <statement-list> } else { <statement-list> } <statement-list> } }",
                "<class> class <id> { <class> static void main ( String []args ) { <statement-list> if ( <condition> ) { <statement-list> } } }",
                "<class> class <id> { <class> static void main ( String []args ) { <statement-list> if ( <condition> ) { <statement-list> } else { <statement-list> } } }",
                "<class> class <id> { <class> static void main ( String []args ) { <statement-list> if ( <condition> ) { } else { <statement-list> } } }",
                "<class> class <id> { <class> static void main ( String []args ) { <statement-list> if ( <condition> ) { <statement-list> } else { } } }",
                "<class> class <id> { <class> static void main ( String []args ) { do { } while ( <condition> ) ; } }",
                "<class> class <id> { <class> static void main ( String []args ) { do { <statement-list> } while ( <condition> ) ; } }",
                "<class> class <id> { <class> static void main ( String []args ) { switch ( <id> ) { case <number> : <statement-list> break ; } } }",
                "<class> class <id> { <class> static void main ( String []args ) { if ( <condition> ) { <statement-list> } } }",
                "<class> class <id> { <class> static void main ( String []args ) { if ( <condition> ) { <statement-list> } else { <statement-list> } } }",
                "<class> class <id> { <statement-list> <class> static void main ( String []args ) { do { } while ( <condition> ) ; } }",
                "<class> class <id> { <statement-list> <class> static void main ( String []args ) { do { <statement-list> } while ( <condition> ) ; } }",
                "<class> class <id> { <statement-list> <class> static void main ( String []args ) { switch ( <id> ) { case <number> : <statement-list> break ; } } }",
                "<class> class <id> { <statement-list> <class> static void main ( String []args ) { if ( <condition> ) { <statement-list> } } }",
                "<class> class <id> { <statement-list> <class> static void main ( String []args ) { if ( <condition> ) { <statement-list> } else { <statement-list> } } }",
                "<class> static void main ( String []args ) { }",
                "<class> static void main ( String []args ) { <statement-list> }",
                "if ( <condition> ) { }",
                "<statement-list> if ( <condition> ) { }",
                "<statement-list> if ( <condition> ) { } <statement-list>",
                "if ( <condition> ) { } <statement-list>",
                "if ( <condition> ) { <statement-list> } <statement-list>",
                //
                "<statement-list> if ( <condition> ) { } else { }",
                "if ( <condition> ) { } else { <statement-list> }",
                "if ( <condition> ) { <statement-list> } else { }",
                "if ( <condition> ) { <statement-list> } else { } <statement-list> ",
                "<statement-list> if ( <condition> ) { } else { } <statement-list>",
                "<statement-list> if ( <condition> ) { <statement-list> } else { <statement-list> } <statement-list>",
                "<statement-list> if ( <condition> ) { } else { <statement-list> } <statement-list>",
                "if ( <condition> ) { } else { } <statement-list>",
                //
                "<statement-list> if ( <condition> ) { } else { <statement-list> }",
                "<statement-list> if ( <condition> ) { <statement-list> } else { }",
                "<statement-list> if ( <condition> ) { } else { <statement-list> } <statement-list>",
                "if ( <condition> ) { } else { <statement-list> } <statement-list>",
                //
                "if ( <condition> ) { <statement-list> }",
                "if ( <condition> ) { <statement-list> } else { <statement-list> }",
                "if ( <condition> ) { } else { }",
                "<statement-list> if ( <condition> ) { <statement-list> }",
                "<statement-list> if ( <condition> ) { <statement-list> } else { <statement-list> }",
                "<statement-list> if ( <condition> ) { } else { }",
                "while ( <condition> ) { }",
                "while ( <condition> ) { <statement-list> }",
                "<statement-list> while ( <condition> ) { }",
                "<statement-list> while ( <condition> ) { <statement-list> }",
                "for ( <statement> <condition> ; <statement> ) { <statement-list> }",
                "for ( <statement> <condition> ; <statement> ) { }",
                "<statement-list> for ( <statement> <condition> ; <statement> ) { <statement-list> }",
                "<statement-list> for ( <statement> <condition> ; <statement> ) { }",
                "do { } while ( <condition> ) ;",
                "do { <statement-list> } while ( <condition> ) ;",
                "<statement-list> do { } while ( <condition> ) ;",
                "<statement-list> do { <statement-list> } while ( <condition> ) ;",
                "<statement-list> switch ( <id> ) { case <number> : <statement-list> break ; }",
                "<statement-list> switch ( <id> ) { case <number> : <statement-list> break ; case <number> : <statement-list> break ; }",
                "<statement-list> switch ( <id> ) { case <number> : <statement-list> break ; case <number> : <statement-list> break ; case <number> : <statement-list> break ; }",
                "switch ( <id> ) { case <number> : <statement-list> break ; }",
                "switch ( <id> ) { case <number> : <statement-list> break ; case <number> : <statement-list> break ; }",
                "switch ( <id> ) { case <number> : <statement-list> break ; case <number> : <statement-list> break ; case <number> : <statement-list> break ; }",
                //
                "switch ( <id> ) { case <number> : break ; }",
                "switch ( <id> ) { case <number> : break ; case <number> : break ; }",
                "switch ( <id> ) { case <number> : break ; case <number> : break ; case <number> : break ; }",
                //
                "if ( <condition> ) { <statement-list> } <statement-list>",
                "if ( <condition> ) { <statement-list> } else { <statement-list> } <statement-list>",
                "if ( <condition> ) { } else { } <statement-list>",
                "<statement-list> if ( <condition> ) { <statement-list> } <statement-list>",
                "<statement-list> if ( <condition> ) { <statement-list> } else { <statement-list> } <statement-list>",
                "<statement-list> if ( <condition> ) { } else { } <statement-list>",
                "while ( <condition> ) { } <statement-list>",
                "while ( <condition> ) { <statement-list> } <statement-list>",
                "<statement-list> while ( <condition> ) { } <statement-list>",
                "<statement-list> while ( <condition> ) { <statement-list> } <statement-list>",
                "for ( <statement> <condition> ; <statement> ) { <statement-list> } <statement-list>",
                "for ( <statement> <condition> ; <statement> ) { } <statement-list>",
                "<statement-list> for ( <statement> <condition> ; <statement> ) { <statement-list> } <statement-list>",
                "<statement-list> for ( <statement> <condition> ; <statement> ) { } <statement-list>",
                "do { } while ( <condition> ) ; <statement-list>",
                "do { <statement-list> } while ( <condition> ) ; <statement-list>",
                "<statement-list> do { } while ( <condition> ) ; <statement-list>",
                "<statement-list> do { <statement-list> } while ( <condition> ) ; <statement-list>",
                "<statement-list> switch ( <id> ) { case <number> : <statement-list> break ; } <statement-list>",
                "<statement-list> switch ( <id> ) { case <number> : <statement-list> break ; case <number> : <statement-list> break ; } <statement-list>",
                "<statement-list> switch ( <id> ) { case <number> : <statement-list> break ; case <number> : <statement-list> break ; case <number> : <statement-list> break ; } <statement-list>",
                //
                 "<statement-list> switch ( <id> ) { case <number> : break ; } <statement-list>",
                "<statement-list> switch ( <id> ) { case <number> : break ; case <number> : break ; } <statement-list>",
                "<statement-list> switch ( <id> ) { case <number> : break ; case <number> : break ; case <number> : break ; } <statement-list>",
                //
                "switch ( <id> ) { case <number> : <statement-list> break ; } <statement-list>",
                "switch ( <id> ) { case <number> : <statement-list> break ; case <number> : <statement-list> break ; } <statement-list>",
                "switch ( <id> ) { case <number> : <statement-list> break ; case <number> : <statement-list> break ; case <number> : <statement-list> break ; } <statement-list>",
                "switch ( <id> ) { case <number> : break ; } <statement-list>",
                "switch ( <id> ) { case <number> : break ; case <number> : break ; } <statement-list>",
                "switch ( <id> ) { case <number> : break ; case <number> : break ; case <number> : break ; } <statement-list>",
                "<statement-list>",
            };
            addStartRules(rules);
        }

        public override void initProductionRules()
        {
            var conditionRules = new List<string>()
            {
                "true",
                "false",
                "<id> <relational-operator> <id>",
                "<id> <relational-operator> <number>",
                "<number> <relational-operator> <number>",
            };

            addProductionRules("<condition>", conditionRules);

            //addProductionRule("<id>", "var1 var2 q w e r t y u i o p a s d f g h j k l ñ z x c v b n m var3 var4 var5 var6 var7 var8 var9" +
            //    "hola asd qwe asdf variable temp jaja adios Thanos");

            addProductionRule("<relational-operator>", "> < == >= <= !=");

            var tipos = new List<string>()
            {
                "<integer>",
                "<double>",
            };
            addProductionRules("<tipos>", tipos);

            var enteros = new List<string>()
            {
                "<digit> <integer>",
                "<digit>",
            };
            addProductionRules("<integer>", enteros);

            addProductionRule("<char>", "q w e r t y u i o p a s d f g h j k l ñ z x c v b n m Q W E R T Y U I O P A S D F G H J K L " +
                "Ñ Z X C V B N M 0 1 2 3 4 5 6 7 8 9");

            addProductionRule("<number>", "0 1 2 3 4 5 6 7 8 9 ");

            var statementRules = new List<string>()
            {
                "System.out.println ( <id> ) ;",
                "System.out.println ( ) ;",
                "System.out.print ( <id> ) ;",
                "System.out.print ( ) ;",
                "String <id> = ' <id> ' ;",
                "String <id> = <id> ;",
                "<type> <id> ;",
                "<type> <id> = <id> ;",
                "<type> <id> = <id> + <id> ;",
                "<type> <id> = <number> ;",
                "<type> <id> = <function-call> ;",
                "<id> = <id> ;",
                "<id> = <number> ;",
                "<id> = <function-call> ;",
                "<function-call> ;",
                "boolean <id> = true ;",
                "double <id> = <number> . <number> ;",
                "boolean <id> = false ;",
                "char <id> = ' <char> ' ;",
                "<id> ++ ;",
                "<id> -- ;",
            };
            var statementListRules = new List<string>()
            {
                "<statement> <statement-list>",
                "<statement>",
            };
            addProductionRules("<statement-list>", statementListRules);

            addProductionRules("<statement>", statementRules);
            addProductionRule("<string>", "String");
            addProductionRule("<type>", "int float double char String boolean");
            addProductionRule("<class>", "public private protected");

            var functionCallRules = new List<string>()
            {
                "<id> ( )",
                "<id> . <id> ( )",
            };
            addProductionRules("<function-call>", functionCallRules);

            addProductionRule("char", "char");
            addProductionRule("System.out.println", "System.out.println");
            addProductionRule("System.out.print", "System.out.print");
            addProductionRule("", "");
            addProductionRule("double", "double");
            addProductionRule("boolean", "boolean");
            addProductionRule("false", "false");
            addProductionRule("true", "true");
            addProductionRule("void", "void");
            addProductionRule("String", "String");
            addProductionRule("[]args", "[]args");
            addProductionRule("static", "static");
            addProductionRule("main", "main");
            addProductionRule("if", "if");
            addProductionRule("else", "else");
            addProductionRule("switch", "switch");
            addProductionRule("case", "case");
            addProductionRule("class", "class");
            addProductionRule("break", "break");
            addProductionRule("do", "do");
            addProductionRule("while", "while");
            addProductionRule("for", "for");
            addProductionRule("++", "++");
            addProductionRule("+", "+");
            addProductionRule("--", "--");
            addProductionRule("=", "=");
            addProductionRule(";", ";");
            addProductionRule("\"", "\"");
            addProductionRule(":", ":");
            addProductionRule(".", ".");
            addProductionRule("(", "(");
            addProductionRule(")", ")");
            addProductionRule("{", "{");
            addProductionRule("}", "}");
            addProductionRule("'", "'");
        }


        public override void initPOS()
        {
            addPOS("<id>");
            addPOS("<relational-operator>");
            addPOS("<number>");
            addPOS("<type>");
            addPOS("<class>");
            addPOS("<char>");
            addPOS("char");
            addPOS("System.out.println");
            addPOS("System.out.print");
            addPOS("--");
            addPOS("+");
            addPOS("++");
            addPOS("double");
            addPOS("for");
            addPOS("do");
            addPOS("while");
            addPOS("boolean");
            addPOS("break");
            addPOS("true");
            addPOS("false");
            addPOS("String");
            addPOS("class");
            addPOS("[]args");
            addPOS("void");
            addPOS("static");
            addPOS("main");
            addPOS("if");
            addPOS("else");
            addPOS("switch");
            addPOS("case");
            addPOS("\"");
            addPOS("=");
            addPOS(";");
            addPOS(":");
            addPOS(".");
            addPOS("(");
            addPOS(")");
            addPOS("{");
            addPOS("}");
            addPOS("'");
        }
    }
}
