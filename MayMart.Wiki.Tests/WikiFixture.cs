using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MayMart.Wiki.Tests
{
    [TestClass]
    public class WikiFixture
    {
        [TestMethod]
        public void EmptyContentTest()
        {
            WikiEngine engine = new WikiEngine();

            Assert.AreEqual(null, engine.Render(null));
            Assert.AreEqual(null, engine.Render(string.Empty));
        }

        [TestMethod]
        public void HeaderOptionsText()
        {
            WikiEngine engine = new WikiEngine();

            engine.RenderingOptions.Headers = HeaderTypes.All;
            Assert.AreEqual("<h1>TEXT</h1><h2>TEXT</h2><h3>TEXT</h3><h4>TEXT</h4><p>=====TEXT</p>", engine.Render("=TEXT\n==TEXT\n===TEXT\n====TEXT\n=====TEXT\n"));

            engine.RenderingOptions.Headers = HeaderTypes.Default;
            Assert.AreEqual("<p>TEXT</p><h2>TEXT</h2><h3>TEXT</h3><h4>TEXT</h4><p>=====TEXT</p>", engine.Render("=TEXT\n==TEXT\n===TEXT\n====TEXT\n=====TEXT\n"));

            engine.RenderingOptions.Headers = HeaderTypes.Header2;
            Assert.AreEqual("<p>TEXT</p><h2>TEXT</h2><p>TEXT</p><p>TEXT</p><p>=====TEXT</p>", engine.Render("=TEXT\n==TEXT\n===TEXT\n====TEXT\n=====TEXT\n"));

            engine.RenderingOptions.Headers = HeaderTypes.Header1 | HeaderTypes.Header2;
            Assert.AreEqual("<h1>TEXT</h1><h2>TEXT</h2><p>TEXT</p><p>TEXT</p><p>=====TEXT</p>", engine.Render("=TEXT\n==TEXT\n===TEXT\n====TEXT\n=====TEXT\n"));
        }

        [TestMethod]
        public void NonStandardhyphenTest()
        {
            WikiEngine engine = new WikiEngine();

            Assert.AreEqual("<p>TEXT-TEXT</p>", engine.Render("TEXT\x2013TEXT"));
            Assert.AreEqual("<p>TEXT \x2014 TEXT</p>", engine.Render("TEXT \x2013 TEXT"));
            
            Assert.AreEqual("<ul><li>TEXT</li></ul>", engine.Render("\x2013 TEXT"));

            Assert.AreEqual("<ul><li>TEXT</li></ul>", engine.Render("\x2013 TEXT"));
        }

        [TestMethod]
        public void FormattingTest()
        {
            WikiEngine engine = new WikiEngine();

            Assert.AreEqual("<p><b>TEXT</b></p>", engine.Render("*TEXT*"));
            Assert.AreEqual("<p><b><i>TEXT</i></b></p>", engine.Render("*_TEXT_*"));
            Assert.AreEqual("<p><b><i>TEXT</i> TEXT</b></p>", engine.Render("*_TEXT_ TEXT*"));
            Assert.AreEqual("<p><b><i><b>TEXT</b></i></b></p>", engine.Render("*_*TEXT*_*"));

            Assert.AreEqual("<p><b>TEXT</b>.</p>", engine.Render("*TEXT*."));
            Assert.AreEqual("<p><i>TEXT</i>.</p>", engine.Render("_TEXT_."));
        }

        [TestMethod]
        public void UrlNormalizationTest()
        {
            WikiEngine engine = new WikiEngine();

            Assert.AreEqual("<p><a href=\"http://www.abc.com\">www.abc.com</a></p>", engine.Render("[url:www.abc.com]"));
            Assert.AreEqual("<p><a href=\"http://www.abc.com\">http://www.abc.com</a></p>", engine.Render("[url:http://www.abc.com]"));
            Assert.AreEqual("<p><a href=\"/link\">/link</a></p>", engine.Render("[url:/link]"));

            Assert.AreEqual("<p><a href=\"mailto:bob@abc.com\">bob@abc.com</a></p>", engine.Render("[mailto:bob@abc.com]"));
        }

        [TestMethod]
        public void SkipLinesTest()
        {
            WikiEngine engine = new WikiEngine();

            engine.RenderingOptions.SkipLinesEnabled = true;
            Assert.AreEqual("<p>TEXT 1</p><p>TEXT 2</p>", engine.Render("TEXT 1\n? COMMENT\nTEXT 2"));

            engine.RenderingOptions.SkipLinesEnabled = false;
            Assert.AreEqual("<p>TEXT 1</p><p>? COMMENT</p><p>TEXT 2</p>", engine.Render("TEXT 1\n? COMMENT\nTEXT 2"));
        }
    }
}
