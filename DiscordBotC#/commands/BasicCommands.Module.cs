using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
//using DSharpPlus.Entities.Discord​Embed​;
using System;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web;
using HtmlAgilityPack;
//using System.Runtime.dll;
/* Create our class and extend from IModule */
public class BasicCommandsModule : IModule
{
    public void test(string e)
    {
        String addresse = /*"https://archiveofourown.org/works/19762567/chapters/46780885"*/ e;
        HtmlWeb webe = new HtmlWeb();
        var htmlDoce = webe.Load(addresse);
        Console.WriteLine("Testing the thing "+(htmlDoce.GetType()));
    }






    [Command("#")]
    [Description("Returns information about the fanfic")]
    public async Task Testing(CommandContext ctx, string address)
    {
        await ctx.TriggerTypingAsync();


        address = address + "?view_adult=true";
        Console.WriteLine(address);

        String Dump = "";
        HtmlWeb web = new HtmlWeb();
        var htmlDoc = web.Load(address);
        //var L = htmlDoc.DocumentNode.SelectSingleNode("//body");

        //("#text")
        //foreach (var n in L.DescendantNodes())
        Console.WriteLine("The nodeSearch output is: "+nodeSearch("class=\"rating tags", " "));
        string nodeSearch(string search, string spacer)
        {
            string toRet = "";
            string myString = "";
            int count = 0;
            var L = htmlDoc.DocumentNode.SelectSingleNode("//body");
            foreach (var p in L.DescendantNodes())
            {
                //Console.WriteLine("p's " + p.OuterHtml +" || "+ p.InnerText+ " || " + p.InnerHtml + "\n");
                if ( p.OuterHtml.Contains(search)) { Console.WriteLine("found: " + p.OuterHtml); }
                foreach (var e in p.Elements(search))
                {
                    Console.WriteLine("e's "+e.InnerText +"  "+ e.Name +"\n");
                    foreach (var n in e.Descendants("#text"))
                    {
                        myString = n.InnerText;
                        //myString = Regex.Replace(myString, " {2,}", " ");
                        if (!String.IsNullOrWhiteSpace(myString))
                        {
                            Console.WriteLine(/*n.Name + "  "+*/ myString);
                            toRet += myString + spacer;
                            count = 0;
                        }
                        else if (count > 2)
                        {
                            Console.WriteLine(/*n.Name + "  "+*/ myString);
                            toRet += myString + spacer;
                            count = 0;
                        }
                        else { count++; }
                    }
                }
                //Console.WriteLine(n.InnerText);
                //Dump = Dump + n.InnerText + "\n";
            }
            return toRet;
        }
        var toPrintSeries = new DiscordEmbedBuilder
        {
            Title = ("Testing everything"), Description = Dump

        };

        //    var toPrintSeries = new Discord​EmbedBuilder
        //    {
        //        Title = (title),
        //        Description = ("**By** " + author +
        // "\n" + "**Stats:** " + stats +
        //"\n" + "**Summary:** \n" + sum + "\n\n")
        //    };

        await ctx.RespondAsync(embed: toPrintSeries);

    }











    //HtmlAgilityPack.HtmlDocument
    [Command("@")]
    [Description("Returns information about the fanfic")]
    public async Task Test(CommandContext ctx, string address)
    {
        string title = "";
        string sum = "";
        string author = "";
        string relations = "";
        string cat = "";
        string warn = "";
        string rating = "";
        int temp = 0;
        Boolean chap = false;
        Boolean series = false;
        Boolean full = false;
        string stats = "";
        await ctx.TriggerTypingAsync();
        if (!address.Contains("archiveofourown")) { }
        else
        {
            //print command info to console
            Console.WriteLine("Guild: " + ctx.Guild.Name);
            Console.WriteLine("Channel: " + ctx.Channel.Name);
            Console.WriteLine("User: " + ctx.User.Username);
            Console.WriteLine(DateTime.Now);
            Console.WriteLine(address + "\n");
            
            
            //cleans off the #workskin tag for ease of reading
            if (address.Contains("#workskin"))
            {
                temp = address.IndexOf("#workskin");
                address = address.Remove(temp);
                //address = address + "?view_full_work=true";
                full = true;
                Console.WriteLine("full = true");
            }


            address = address + "?view_adult=true";
            Console.WriteLine(address);

            series = (address.Contains("/series/"));
            chap = (address.Contains("chapters"));

            
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(address);
            string myString;
            test(address);
            if (chap)
            {
                Console.WriteLine("chap");
                ////title = htmlParseGeneric("//*[@id=\"workskin\"]/div[1]/h2", " ");
                if (full)
                {

                    ////sum = htmlParseGeneric("//*[@id=\"summary\"]/blockquote}", "\n");
                }
                else
                {
                    ////sum = htmlParseGeneric("//*[@id=\"workskin\"]/div[1]/div[1]/blockquote", "\n") + "\n";
                }//*[@id="workskin"]/div[1]/h3
                author = htmlParse("//*[@id=\"workskin\"]/div[1]/h3/a");
                ////relations = htmlParseGeneric("//*[@id=\"main\"]/div[2]/div[1]/dl/dd[5]/ul", " ");
                ////cat = htmlParseGeneric("//*[@id=\"main\"]/div[2]/div[1]/dl/dd[3]/ul", " ");
                ////warn = htmlParseGeneric("//*[@id=\"main\"]/div[2]/div[1]/dl/dd[2]/ul", " ");
                rating = htmlParse("//*[@id=\"main\"]/div[2]/div[1]/dl/dd[1]/ul");
            }
            else if (series)
            {
                Console.WriteLine("series");
                title = htmlParse("//*[@id=\"main\"]/h2");
                ////sum = htmlParseGeneric("//*[@id=\"main\"]/div[2]/dl/dd[4]/blockquote/p", "\n") + "\n";
                author = htmlParse("//*[@id=\"main\"]/div[2]/dl/dd[1]");
                stats = htmlParse("//*[@id=\"main\"]/div[2]/dl/dd[5]/dl");

            }
            else
            {
                Console.WriteLine("else");
                title = htmlParse("//*[@id=\"workskin\"]/div[1]/h2");
                /////sum = htmlParseGeneric("//*[@id=\"workskin\"]/div[1]/div[1]/blockquote", "\n") + "\n";
                author = htmlParse("//*[@id=\"workskin\"]/div[1]/h3/a");
                relations = htmlParse("//*[@id=\"main\"]/div[2]/dl/dd[5]/ul");
                cat = htmlParse("//*[@id=\"main\"]/div[2]/dl/dd[3]/ul");
                warn = htmlParse("//*[@id=\"main\"]/div[2]/dl/dd[2]/ul");
                rating = htmlParse("//*[@id=\"main\"]/div[2]/dl/dd[1]/ul");
            }

            Console.WriteLine(title + " | " + sum + " | " + author + " | " + relations + " | " + cat + " | " + warn + " | " + rating);


            if (!series)
            {
                var toPrint = new Discord​EmbedBuilder
                {
                    Title = (title),
                    Description = ("**By** " + author +
                    "\n" + "**Rating:** " + rating +
                    "\n" + "**Archive Warnings:** " + warn +
                    "\n" + "**Catagory:** " + cat +
                    "\n" + "**Relationships:** " + relations +
                    "\n" + "**Summary:** \n" + sum + "\n\n")
                };

                await ctx.RespondAsync(embed: toPrint);
            }
            else
            {
                var toPrintSeries = new Discord​EmbedBuilder
                {
                    Title = (title),
                    Description = ("**By** " + author +
                     "\n" + "**Stats:** " + stats +
                    "\n" + "**Summary:** \n" + sum + "\n\n")
                };

                await ctx.RespondAsync(embed: toPrintSeries);
            }


            ////string[] relationsArray = new string[]
            ////    {
            ////        "//*[@id=\"main\"]/div[2]/dl/dd[5]/ul",
            ////    "//*[@id=\"main\"]/div[2]/div[1]/dl/dd[3]/ul",
            ////   "//*[@id=\"main\"]/div[2]/div[1]/dl/dd[5]/ul",
            ////   "//*[@id=\"main\"]/div[2]/div[1]/dl/dd[5]"

            ////   //*[@id="main"]/div[2]/dl/dd[5]
            ////    };


            string htmlParse(string input)
            {

                // string myString = "";
                // string output = "";
                // var N = htmlDoc.DocumentNode.SelectSingleNode(input);
                // try
                // {
                //     foreach (var n in N.Descendants("#text"))
                //     {
                //         myString = n.InnerText;
                //         //myString = Regex.Replace(myString, " {2,}", " ");
                //         if (!String.IsNullOrWhiteSpace(myString))
                //         {
                //             Console.WriteLine(/*n.Name + "  "+*/ myString);
                //             output += myString +"  ";
                //         }
                //     }
                //     output = output.Remove(output.LastIndexOf("  "));
                // }
                // catch { Console.WriteLine("Parse failed at "+input); output = "undefined";}
                //output = output.Replace("&amp;", "&");

                return htmlParseGeneric(input, " ");
            }

            string htmlParseGeneric(string input, string spacer)
            {
                string myString = "";
                string output = "";
                Boolean spc = false;
                int count = 0;
                var N = htmlDoc.DocumentNode.SelectSingleNode(input);
                try
                {
                    foreach (var n in N.Descendants("#text"))
                    {
                        myString = n.InnerText;
                        //myString = Regex.Replace(myString, " {2,}", " ");
                        if (!String.IsNullOrWhiteSpace(myString))
                        {
                            Console.WriteLine(/*n.Name + "  "+*/ myString);
                            output += myString + spacer;
                            count = 0;
                        }
                        else if (count > 2)
                        {
                            Console.WriteLine(/*n.Name + "  "+*/ myString);
                            output += myString + spacer;
                            count = 0;
                        }
                        else { count++; }
                    }

                    output = output.Remove(output.LastIndexOf(spacer));
                }
                catch { Console.WriteLine("Parse failed at " + input); output = "undefined"; }
                output = output.Replace("&amp;", "&");
                return output;
            }

            string[] htmlParseArray(string input)
            {
                string myString = "";
                string[] output = new string[100];
                int i = 0;
                var N = htmlDoc.DocumentNode.SelectSingleNode(input);
                try
                {
                    foreach (var n in N.Descendants("#text"))
                    {
                        myString = n.InnerText;
                        //myString = Regex.Replace(myString, " {2,}", " ");
                        if (!String.IsNullOrWhiteSpace(myString))
                        {
                            Console.WriteLine(/*n.Name + "  "+*/ myString);
                            output[i] = myString;
                            i++;
                        }
                    }
                }
                catch { Console.WriteLine("Parse failed at " + input); }
                return output;
            }



        }
    }
                



















    /* Commands in DSharpPlus.CommandsNext are identified by supplying a Command attribute to a method in any class you've loaded into it. */
    /* The description is just a string supplied when you use the help command included in CommandsNext. */
    //[Command("alive")]
    //[Description("Simple command to test if the bot is running!")]
    //public async Task Alive(CommandContext ctx)
    //{
    //    /* Trigger the Typing... in discord */
    //    await ctx.TriggerTypingAsync();

    //    /* Send the message "I'm Alive!" to the channel the message was recieved from */
    //    await ctx.RespondAsync("I'm alive!");
    //}

    // Oscar Jara https://stackoverflow.com/questions/10709821/find-text-in-string-with-c-sharp

    public static string getBetween(string strSource, string strStart, string strEnd)
    {
        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
        {
            int Start, End;
            Start = strSource.IndexOf(strStart, 0) + strStart.Length;
            End = strSource.IndexOf(strEnd, Start);
            return strSource.Substring(Start, End - Start);
            // Console.WriteLine("Does contain: " + strSource + "  " + strStart + " " + strEnd);

        }
        // Console.WriteLine("Does not contain: "+strSource +"  " + strStart + " "+strEnd );
        return "";
    }


    // Modify to return everything else
    public static string parseParagraph(string strSource, string strStart, string strEnd, string add)
    {
        //Console.WriteLine("Source is : " + strSource);
        string output = "";
        string temp = "some nonsence";
        int i = 0;
        do //cover to for or break
        {
            temp = getBetween(strSource, strStart, strEnd);
            if (temp == ""&& strSource == "") { break; }
            output = (output+ temp + add); 
           // strSource = removeString(strSource, getBetween(strSource, strEnd, strStart));

            strSource = removeString(strSource, (strStart + temp+ strEnd));
            i++;
            //Console.WriteLine("parseParagraph: i: " +i +"output: " + output +"|"+ "StrSource: "+strSource);   
        }

        while ((temp != "" ) && i<100);
 
        return output;
    }

    public static List<string> seperate(string strSource, string strStart, string strEnd)
    {
       //Regex r = new Regex("\\s");
        List<string> output= new List<string>();
        string temp = "some nonsence";
        int i = 0;
        do //change to for due to last blank line
        {
            temp = getBetween(strSource, strStart, strEnd);
          //  Console.WriteLine("seperate: i: " + i + " temp: " +  temp +"|");
            //if (r.IsMatch(temp))
            //{

            //}
            //else
            //{
            output.Add(temp);
            //}
            strSource = removeString(strSource, (strStart + temp + strEnd));
            i++;
            // Console.WriteLine(output.Count);
        }
        while (temp != "" && i < 100);

        return output;
    }

    // LukeH https://stackoverflow.com/questions/2201595/c-sharp-simplest-way-to-remove-first-occurrence-of-a-substring-from-another-st
    public static string removeString(string start, string remove)
    {

        int index = start.IndexOf(remove);
        string cleanPath = (index < 0)
            ? start
            : start.Remove(index, remove.Length);
        return cleanPath;
    }

    public static string htmlCleanup(string toClean)
    {

        return toClean;
    }
    private bool isWarning(String code)
    {
        if (code.Contains( "This work could have adult content. If you proceed you have agreed that you are willing to see such content."))
            return true;
        return false;
    }




















    [Command("f")]
    [Description("Returns information about the fanfic")]
    public async Task Link(CommandContext ctx, string address)
    {
        Console.WriteLine("Guild: " + ctx.Guild.Name);
        Console.WriteLine("Channel: " + ctx.Channel.Name);
        Console.WriteLine("User: " +  ctx.User.Username);
        Console.WriteLine(DateTime.Now);
        Console.WriteLine(address + "\n");
        

        using (WebClient client = new WebClient())
        {
            /* Trigger the Typing... in discord */
            await ctx.TriggerTypingAsync();
            address = address + "?view_adult=true";
            
            string htmlCode = client.DownloadString(address);
           
            string testHTML = getBetween(htmlCode, "!-- END navigation -->", "<!--main content-->");
            if (testHTML == "")
            {
                testHTML = getBetween(htmlCode, "!-- END navigation -->", "<!--chapter content-->");
                //<!--chapter content-->
                if (testHTML == "")
                {
                    Console.WriteLine("This link has failed: " + address);
                    await ctx.RespondAsync("This failed for some raisin");
                }
                else
                {
                    htmlCode = testHTML;
                }
            }
            else
            {
                htmlCode = testHTML;
            }

            htmlCode = HttpUtility.HtmlDecode(htmlCode);
            //bool warn= isWarning(htmlCode);
            // string[] words = url.Split('/');
            https://archiveofourown.org/works/12191370/chapters/27679647


            // Requested items:
            //Title - check
            //Rating - check 
            //Archive Warnings - check 
            //Catagory - check 
            //Relationships - check
            //Author - check
            //Summary - check

            // !-- END navigation --> to <!--main content-->


            string authorName = getBetween(htmlCode, "<a rel=", ">");
            // refine?
            authorName = getBetween(authorName, "/users/", "/pseuds/");

            string workskin = getBetween(htmlCode, "<!-- BEGIN section where work skin applies -->", "</div>");
            //Console.WriteLine("Workskin "+workskin);



            // Archive warnings: 
            // <dd class="warning tags"> to </dd>
            // take each href= to </li>
            // for each, take > to </a>
            string archive = getBetween(htmlCode, "<dd class=\"warning tags\">", "</dd>");
            //Console.WriteLine(" 1st "+archive);
            List<string> arcArray = seperate(archive, "href=", "</li>");
            //Console.WriteLine("test");
            //arcArray.ForEach(delegate (string name)
            //{
            //    Console.WriteLine("hehe "+name);
            //});
            //foreach (string e in arcArray)
            //{
            //    Console.WriteLine(" 2nd " + e);
            //}

            archive = "";
            for (int i = 0; arcArray[i] != ""; i++)
            {
                if (arcArray[i] != "")
                {
                    archive = archive + parseParagraph(arcArray[i], ">", "</a>", " ");
                    //Console.WriteLine(" test "+parseParagraph(arcArray[i], ">", "</a>", ""));
                }
            }
            //Console.WriteLine("This should be the archive: "+archive);

            //Category
            string cat = getBetween(htmlCode, "<dd class=\"category tags\">", "</dd>");
            //Console.WriteLine(" 1st "+archive);
            List<string> catArray = seperate(cat, "href=", "</li>");
            //foreach (string e in arcArray)
            //{
            //    Console.WriteLine(" 2nd " + e);
            //}

            cat = "";
            for (int i = 0; catArray[i] != ""; i++)
            {
                if (catArray[i] != "")
                {
                    cat = cat + parseParagraph(catArray[i], ">", "</a>", " ");
                    //Console.WriteLine(parseParagraph(arcArray[i], ">", "</a>", ""));
                }
            }

            if (cat == "")
            {
                cat = "No Catagory";
            }
            //Console.WriteLine("This should be the archive: "+archive);



            //Relationships
            string relation = getBetween(htmlCode, "<dd class=\"relationship", "</dd>");
            //Console.WriteLine(" 1st "+relation);
            List<string> relationArray = seperate(relation, "href=", "</li>");
            //foreach (string e in relationArray)
            //{
            //    Console.WriteLine(" 2nd " + e);
            //}

            relation = "";
            for (int i = 0; relationArray[i] != ""; i++)
            {
                if (relationArray[i] != "")
                {
                    relation = relation + parseParagraph(relationArray[i], ">", "</a>", " ");

                }
            }




            //Rating
            // < dd class=\"rating tags\"> to </dd>
            // then /works"> to </a>
            string rating = getBetween(htmlCode, "<dd class=\"rating tags\">", "</dd>");
            // Console.WriteLine(rating);
            rating = getBetween(rating, "/works\">", "</a>");





            string title = getBetween(htmlCode, "<h2 class=\"title heading\">", "</h2>");

            //// Patrick Desjardins https://stackoverflow.com/questions/206717/how-do-i-replace-multiple-spaces-with-a-single-space-in-c
            //RegexOptions options = RegexOptions.None;
            //Regex regex = new Regex("[ ]{2,}", options);
            //title = regex.Replace(title, " ");

            //title = Regex.Replace(title, @"\t|\n|\r", "");

            //Console.WriteLine("Title "+title);

            //title = getBetween(title, ">","<");
            //Console.WriteLine(title);
            // "rel = \"author\" href = \"/users/"
            // Console.WriteLine(authorName);
            string desc = "";
            
            string descOld = getBetween(htmlCode, "class=\"userstuff\">", "</div>");
            if (descOld == "")
            {
                descOld = getBetween(workskin, "userstuff", "</blockquote>");
            }
            descOld = descOld.Replace("<p></p><blockquote>", "_");
                
            //desc = HttpUtility.HtmlDecode(descOld);
            //Console.WriteLine("Pre game : \n" + desc);
            desc = parseParagraph(descOld, "<p>", "</p>", "\n");    
            desc = desc.Replace("<br/>", "\n");
            //Console.WriteLine("before: " + desc);
            //desc = HttpUtility.HtmlDecode(desc);

            //relation = HttpUtility.HtmlDecode(relation);
            {
                /*
                if (warn)
                {
                    string descNew = getBetween(htmlCode, "userstuff", "</blockquote>");
                    desc = parseParagraph(descNew, "<p>", "</p>");
                    //Console.WriteLine("Used new: "+ desc);
                }
                else
                {
                    string descOld = getBetween(workskin, "userstuff", "</blockquote>");
                    desc = parseParagraph(descOld, "<p>", "</p>");
                }
                */

                //Console.WriteLine(desc + "summary");


            }
            var toPrint = new Discord​EmbedBuilder
            {
                Title = (title),
                Description = ("**By** " + authorName +
                "\n" + "**Rating:** " + rating +
                "\n" + "**Archive Warnings:** " + archive +
                "\n" + "**Catagory:** " + cat +
                "\n" + "**Relationships:** " + relation +
                
                //"\n" + "Fanfiction author is " + authorName +
                "\n" + "**Summary:** \n" + desc + "\n\n")
            } ;

            await ctx.RespondAsync(embed: toPrint);
            //test.AddField("testField", title, true);
            //await ctx.RespondAsync($"Embed test: "+ test);


            //await ctx.RespondAsync($"Title is:"+ title + 
            //    "\n" + "Rating is: " + rating + 
            //    "\n" + "Catagory: "+ cat +
            //    "\n" + "Relationships: " + relation + 
            //    "\n" + "Archive warnings: " + archive+
            //    "\n" + "Fanfiction author is " + authorName +
            //    "\n" + "Description: \n" + desc + "\n\n");


            //await ctx.RespondAsync($"Title is:"+ title);
            //await ctx.RespondAsync($"Rating is: "+ rating);
            //await ctx.RespondAsync($"Catagory: "+ cat);
            //await ctx.RespondAsync($"Relationships: "+ relation);
            //await ctx.RespondAsync($"Archive warnings: "+ archive);
            //await ctx.RespondAsync($"Fanfiction author is "+ authorName);
            //await ctx.RespondAsync($"Description: \n"+ desc +"\n\n");
            // { ctx.User.Username}
        }
    }
    //[Command("interact")]
    //[Description("Simple command to test interaction!")]
    //public async Task Interact(CommandContext ctx)
    //{
    //    /* Trigger the Typing... in discord */
    //    await ctx.TriggerTypingAsync();

    //    /* Send the message "I'm Alive!" to the channel the message was recieved from */
    //    await ctx.RespondAsync("How are you today?");

    //    var intr = ctx.Client.GetInteractivityModule(); // Grab the interactivity module
    //    var reminderContent = await intr.WaitForMessageAsync(
    //        c => c.Author.Id == ctx.Message.Author.Id, // Make sure the response is from the same person who sent the command
    //        TimeSpan.FromSeconds(60) // Wait 60 seconds for a response instead of the default 30 we set earlier!
    //    );
    //    // You can also check for a specific message by doing something like
    //    // c => c.Content == "something"

    //    // Null if the user didn't respond before the timeout
    //    if (reminderContent == null)
    //    {
    //        await ctx.RespondAsync("Sorry, I didn't get a response!");
    //        return;
    //    }

    //    // Homework: have this change depending on if they say "good" or "bad", etc.
    //    await ctx.RespondAsync("Thank you for telling me how you are!");
    //}
}