using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OverEngineering.Domain;
using OverEngineering.Logic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class MeasureParserTests
    {
        [TestMethod]
        public async Task TestHtmlTemperatureParsing()
        {
            var collector = new Mock<IRawMeasuresCollector>();
            collector
                .Setup(c => c.CollectRawMeasurement(MeasureType.Temperature))
                .Returns(Task.FromResult(_temperatureDataA));

            var actual = (await new MeasureParser(collector.Object)
                .GetMeasures(MeasureType.Temperature)
                ).ToArray();

            Assert.AreEqual(165, actual.Length);
            Assert.AreEqual(7.1M, actual.First(l => l.Date == DateTime.Parse("24.11.2020 10:15")).Value);
        }

        [TestMethod]
        public async Task TestHtmlLevelParsing()
        {
            var collector = new Mock<IRawMeasuresCollector>();
            collector
                .Setup(c => c.CollectRawMeasurement(MeasureType.Level))
                .Returns(Task.FromResult(_levelDataA));

            var actual = (await new MeasureParser(collector.Object)
                .GetMeasures(MeasureType.Level)
                ).ToArray();

            Assert.AreEqual(165, actual.Length);
            Assert.AreEqual(68M, actual.First(l=>l.Date == DateTime.Parse("24.11.2020 10:15")).Value);
        }

        #region Test data

        private static string _temperatureDataA = @"<!DOCTYPE html>
<html lang=""de"">
<head>
    <meta charset=""utf-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
    <title>Wassertemperatur: Aktuelle Messwerte München Tierärztl. Hochschule / Schwabinger Bach</title>
    <link href=""https://www.gkd.bayern.de/css/style.css"" rel=""stylesheet"">
    <link rel=""icon"" href=""https://www.gkd.bayern.de/favicon.ico"">
    <link rel=""canonical"" href=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur/isar/muenchen-tieraerztl-hochschule-16516008/messwerte/tabelle"" />
    <meta name=""description"" content=""""><meta name=""keywords"" content=""Gewässerkundlicher Dienst,Bayern,Messdaten,Gewässerkunde,Download,Abfluss,Grundwasser,Grundwasserstand,Niederschlag,Wassertemperatur,Fluss,See,Gewaesser,Wasserstand,GKD,Gewässerchemie""><meta name=""robots"" content=""index, follow""><link href=""https://www.gkd.bayern.de/files/js/jquery-ui-1.12.1/jquery-ui.min.css"" rel=""stylesheet""><link href=""https://www.gkd.bayern.de/files/js/jquery-ui-1.12.1/jquery-ui.min.css"" rel=""stylesheet""><script src=""https://www.gkd.bayern.de/files/js/jquery-3.3.1/jquery.min.js""></script><script src=""https://www.gkd.bayern.de/files/js/jquery/jquery.maphilight.min.js""></script></head>

<body>
<a title=""#"" name=""top""></a>
<div id=""center"" class=""wide"">
    <div id=""kopf"">
        <div id=""schriftzug"">
            <div id=""navi_meta""><ul><li class=""""><a href=""https://www.gkd.bayern.de/de/"" title=""Startseite"" id=""Startseite"">Startseite</a></li><li class="" append_mobile""><a href=""https://www.gkd.bayern.de/de/kontakt"" title=""Kontakt"" id=""Kontakt"">Kontakt</a></li><li class="" append_mobile""><a href=""https://www.gkd.bayern.de/de/impressum"" title=""Impressum"" id=""Impressum"">Impressum</a></li><li class="" append_mobile""><a href=""https://www.gkd.bayern.de/de/datenschutz"" title=""Datenschutz"" id=""Datenschutz"">Datenschutz</a></li><li class=""""><a href=""http://www.lfu.bayern.de/"" target=""_blank"" id=""LfU-Hauptangebot"">LfU-Hauptangebot</a></li></ul></div>
            <div id=""lfu""><img src=""https://www.gkd.bayern.de/images/layout/schriftzug_l.png"" alt=""Bayerisches Landesamt f&uuml;r Umwelt""></div>
        </div>
        <div id=""kopfgrafik"">Gewässerkundlicher Dienst Bayern</div>
        <div id=""navi_horizontal_container"">
            <div id=""navi_horizontal"">
                <ul><li class=""active""><a href=""https://www.gkd.bayern.de/de/fluesse"" class=""active"" id=""Fl&uuml;sse"">Fl&uuml;sse</a><ul class=""hide""><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/wasserstand"" id=""Wasserstand"">Wasserstand</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/abfluss"" id=""Abfluss"">Abfluss</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur"" id=""Wassertemperatur"">Wassertemperatur</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/schwebstoff"" id=""Schwebstoff"">Schwebstoff</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/chemie"" id=""Chemie"">Chemie</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/biologie"" id=""Biologie"">Biologie</a></li></ul></li><li class=""""><a href=""https://www.gkd.bayern.de/de/seen"" id=""Seen"">Seen</a><ul class=""hide""><li class=""""><a href=""https://www.gkd.bayern.de/de/seen/wasserstand"" id=""Wasserstand"">Wasserstand</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/seen/wassertemperatur"" id=""Wassertemperatur"">Wassertemperatur</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/seen/chemie"" id=""Chemie"">Chemie</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/seen/biologie"" id=""Biologie"">Biologie</a></li></ul></li><li class=""""><a href=""https://www.gkd.bayern.de/de/meteo"" id=""Meteorologie"">Meteorologie</a><ul class=""hide""><li class=""""><a href=""https://www.gkd.bayern.de/de/meteo/niederschlag"" id=""Niederschlag"">Niederschlag</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/meteo/schnee"" id=""Schnee"">Schnee</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/meteo/lufttemperatur"" id=""Lufttemperatur"">Lufttemperatur</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/meteo/luftfeuchte"" id=""Relative Luftfeuchte"">Relative Luftfeuchte</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/meteo/wind"" id=""Wind"">Wind</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/meteo/globalstrahlung"" id=""Globalstrahlung"">Globalstrahlung</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/meteo/luftdruck"" id=""Luftdruck"">Luftdruck</a></li></ul></li><li class=""""><a href=""https://www.gkd.bayern.de/de/grundwasser"" id=""Grundwasser"">Grundwasser</a><ul class=""hide""><li class=""""><a href=""https://www.gkd.bayern.de/de/grundwasser/oberesstockwerk"" id=""Wasserstand oberes Stockwerk"">Wasserstand oberes Stockwerk</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/grundwasser/tieferestockwerke"" id=""Wasserstand tiefere Stockwerke"">Wasserstand tiefere Stockwerke</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/grundwasser/quellschuettung"" id=""Quellsch&uuml;ttung"">Quellsch&uuml;ttung</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/grundwasser/quelltemperatur"" id=""Quelltemperatur"">Quelltemperatur</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/grundwasser/chemie"" id=""Chemie"">Chemie</a></li></ul></li><li class="" float_right""><a href=""https://www.gkd.bayern.de/de/downloadcenter"" id=""downloadcenter"">Downloadcenter (<span class=""downloadanz"">0</span>)</a><ul class=""hide""><li class=""""><a href=""https://www.gkd.bayern.de/de/downloadcenter"" id=""Download-Korb"">Download-Korb</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/downloadcenter/wizard"" id=""Auswahl über Karte"">Auswahl über Karte</a></li></ul></li></ul>            </div>

            <div id=""navi_horizontal_sub"">
                <ul><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/wasserstand"" id=""Wasserstand"">Wasserstand</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/abfluss"" id=""Abfluss"">Abfluss</a></li><li class=""active""><a href=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur"" class=""active"" id=""Wassertemperatur"">Wassertemperatur</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/schwebstoff"" id=""Schwebstoff"">Schwebstoff</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/chemie"" id=""Chemie"">Chemie</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/biologie"" id=""Biologie"">Biologie</a></li><li> &nbsp;  &nbsp; </li></ul>            </div>
        </div>
    </div>

    <div id=""content"">
        <div id=""surfpfad"">
            <ul>
                <li><a href=""https://www.gkd.bayern.de/de/"">Startseite</a> &gt;</li><li> <a href=""https://www.gkd.bayern.de/de/fluesse"">Fl&uuml;sse</a> &gt;</li><li> <a href=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur"">Wassertemperatur</a> &gt;</li><li> <a href=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur/isar"">Isar</a> &gt;</li><li> <a href=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur/isar/muenchen-tieraerztl-hochschule-16516008"">München Tierärztl. Hochschule</a> &gt;</li><li> Aktuelle Messwerte</li>
            </ul>

            <div id=""kopfsymbole"">
                <ul style=""padding-left:15px;"">
                    <li><a href=""https://www.gkd.bayern.de/en/rivers/watertemperature/isar/muenchen-tieraerztl-hochschule-16516008/current-values/table""><img alt=""English version"" title=""English version"" src=""https://www.gkd.bayern.de/images/symbole/gb.png"" /></a></li>                </ul>
                <form action=""https://www.gkd.bayern.de/de/search"" method=""get"" name=""searchform"">
                    <input onclick=""if(this.value=='Suchbegriff') this.value='';"" type=""text"" name=""suche"" id=""q""
                           value=""Suchbegriff"" size=""18"" maxlength=""128""/>
                    <input type=""submit"" id=""submit"" value=""&gt;&gt;""/>
                </form>
            </div>
            <!--/UdmComment-->

            <!-- id = surfpfad -->
        </div>
        <!-- Janus Header End -->


        <div id=""navi_links_3c""><div class=""row""><div class=""col"">
<div class=""header"">Wassertemperatur</div>
<h4 style=""margin: 5px 0 5px 5px"">München Tierärztl. Hochschule / Schwabinger Bach</h4>
<ul><li><a class="""" href=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur/isar/muenchen-tieraerztl-hochschule-16516008"">Stammdaten / Bild / Karte</a></li>
<li><a class=""active"" href=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur/isar/muenchen-tieraerztl-hochschule-16516008/messwerte"">Aktuelle Messwerte</a></li>
<li><a class="""" href=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur/isar/muenchen-tieraerztl-hochschule-16516008/monatswerte"">Monatsgrafik</a></li>
<li><a class="""" href=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur/isar/muenchen-tieraerztl-hochschule-16516008/jahreswerte"">Jahresgrafik</a></li>
<li><a class="""" href=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur/isar/muenchen-tieraerztl-hochschule-16516008/gesamtzeitraum"">Gesamtzeitraum</a></li>
<li><a class="""" href=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur/isar/muenchen-tieraerztl-hochschule-16516008/download"">Datendownload</a></li>
</ul><h4 style=""margin: 15px 0 5px 5px"">Ansicht</h4><ul><li><a href=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur/isar/muenchen-tieraerztl-hochschule-16516008/messwerte?beginn=23.11.2020&ende=24.11.2020"">Diagramm</a></li><li><a href=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur/isar/muenchen-tieraerztl-hochschule-16516008/messwerte/tabelle?beginn=23.11.2020&ende=24.11.2020"" class=""active"">Tabelle</a></li></ul><h4 style=""margin: 15px 0 5px 5px"">Weitere Messwerte</h4><ul><li><a href=""https://www.gkd.bayern.de/de/fluesse/abfluss/isar/muenchen-tieraerztl-hochschule-16516008/messwerte"">Abfluss</a></li><li><a href=""https://www.gkd.bayern.de/de/fluesse/wasserstand/isar/muenchen-tieraerztl-hochschule-16516008/messwerte"">Wasserstand</a></li><li><a href=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur/isar/muenchen-tieraerztl-hochschule-16516008/messwerte"" class=""active"">Wassertemperatur</a></li></ul></div></div></div>        <div id=""content_3c"" class=""col3""><div class=""row"">
	<div class=""heading""><h1>Aktuelle Messwerte München Tierärztl. Hochschule / Schwabinger Bach</h1></div>
	<div class=""col"">Wassertemperatur vom 23.11.2020 bis zum  24.11.2020<div style=""margin-top:20px;margin-right:5px;""><form  action=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur/isar/muenchen-tieraerztl-hochschule-16516008/messwerte/tabelle"" method=""GET""><input type=""hidden"" name=""zr"" value=""woche""><table id=""downloadtable"" style=""border:0px;"" cellspacing=""0"" cellpadding=""0"" border=""0""><tr><td>Beginn</td><td><input type=""text"" id=""beginn"" name=""beginn"" class=""tcal"" value=""23.11.2020"" /></td><td>Ende</td><td><input type=""text"" id=""ende"" name=""ende"" class=""tcal"" value=""24.11.2020"" /></td><td><input id=""dlbutton"" type=""button"" name=""dl"" style=""width=20px;background-color:#C0C0C0;background-image:url('https://www.gkd.bayern.de/images/layout/okay.png');background-repeat:no-repeat;background-position:center"" onclick=""this.form.submit();"" /></td></tr></table></form></div><table  class=""tblsort""><caption>Wassertemperatur Werte vom 23.11.2020 bis zum  24.11.2020</caption><thead><tr><th  data-sorter=""shortDate"">Datum</th><th  class=""center"">Wassertemperatur [°C]</th></tr></thead><tbody><tr  class=""row""><td >24.11.2020 17:00</td><td  class=""center"">7,0</td></tr><tr  class=""row2""><td >24.11.2020 16:45</td><td  class=""center"">7,0</td></tr><tr  class=""row""><td >24.11.2020 16:30</td><td  class=""center"">7,0</td></tr><tr  class=""row2""><td >24.11.2020 16:15</td><td  class=""center"">7,0</td></tr><tr  class=""row""><td >24.11.2020 16:00</td><td  class=""center"">7,0</td></tr><tr  class=""row2""><td >24.11.2020 15:45</td><td  class=""center"">6,9</td></tr><tr  class=""row""><td >24.11.2020 15:30</td><td  class=""center"">6,9</td></tr><tr  class=""row2""><td >24.11.2020 15:15</td><td  class=""center"">6,9</td></tr><tr  class=""row""><td >24.11.2020 15:00</td><td  class=""center"">7,0</td></tr><tr  class=""row2""><td >24.11.2020 14:45</td><td  class=""center"">7,0</td></tr><tr  class=""row""><td >24.11.2020 14:30</td><td  class=""center"">7,0</td></tr><tr  class=""row2""><td >24.11.2020 14:15</td><td  class=""center"">6,9</td></tr><tr  class=""row""><td >24.11.2020 14:00</td><td  class=""center"">7,0</td></tr><tr  class=""row2""><td >24.11.2020 13:45</td><td  class=""center"">7,0</td></tr><tr  class=""row""><td >24.11.2020 13:30</td><td  class=""center"">7,0</td></tr><tr  class=""row2""><td >24.11.2020 13:15</td><td  class=""center"">7,0</td></tr><tr  class=""row""><td >24.11.2020 13:00</td><td  class=""center"">7,0</td></tr><tr  class=""row2""><td >24.11.2020 12:45</td><td  class=""center"">7,0</td></tr><tr  class=""row""><td >24.11.2020 12:30</td><td  class=""center"">7,0</td></tr><tr  class=""row2""><td >24.11.2020 12:15</td><td  class=""center"">7,1</td></tr><tr  class=""row""><td >24.11.2020 12:00</td><td  class=""center"">7,0</td></tr><tr  class=""row2""><td >24.11.2020 11:45</td><td  class=""center"">7,0</td></tr><tr  class=""row""><td >24.11.2020 11:30</td><td  class=""center"">7,0</td></tr><tr  class=""row2""><td >24.11.2020 11:15</td><td  class=""center"">7,0</td></tr><tr  class=""row""><td >24.11.2020 11:00</td><td  class=""center"">7,0</td></tr><tr  class=""row2""><td >24.11.2020 10:45</td><td  class=""center"">7,0</td></tr><tr  class=""row""><td >24.11.2020 10:30</td><td  class=""center"">7,0</td></tr><tr  class=""row2""><td >24.11.2020 10:15</td><td  class=""center"">7,1</td></tr><tr  class=""row""><td >24.11.2020 10:00</td><td  class=""center"">7,1</td></tr><tr  class=""row2""><td >24.11.2020 09:45</td><td  class=""center"">7,1</td></tr><tr  class=""row""><td >24.11.2020 09:30</td><td  class=""center"">7,1</td></tr><tr  class=""row2""><td >24.11.2020 09:15</td><td  class=""center"">7,1</td></tr><tr  class=""row""><td >24.11.2020 09:00</td><td  class=""center"">7,2</td></tr><tr  class=""row2""><td >24.11.2020 08:45</td><td  class=""center"">7,2</td></tr><tr  class=""row""><td >24.11.2020 08:30</td><td  class=""center"">7,2</td></tr><tr  class=""row2""><td >24.11.2020 08:15</td><td  class=""center"">7,3</td></tr><tr  class=""row""><td >24.11.2020 08:00</td><td  class=""center"">7,3</td></tr><tr  class=""row2""><td >24.11.2020 07:45</td><td  class=""center"">7,3</td></tr><tr  class=""row""><td >24.11.2020 07:30</td><td  class=""center"">7,3</td></tr><tr  class=""row2""><td >24.11.2020 07:15</td><td  class=""center"">7,3</td></tr><tr  class=""row""><td >24.11.2020 07:00</td><td  class=""center"">7,3</td></tr><tr  class=""row2""><td >24.11.2020 06:45</td><td  class=""center"">7,3</td></tr><tr  class=""row""><td >24.11.2020 06:30</td><td  class=""center"">7,3</td></tr><tr  class=""row2""><td >24.11.2020 06:15</td><td  class=""center"">7,3</td></tr><tr  class=""row""><td >24.11.2020 06:00</td><td  class=""center"">7,3</td></tr><tr  class=""row2""><td >24.11.2020 05:45</td><td  class=""center"">7,4</td></tr><tr  class=""row""><td >24.11.2020 05:30</td><td  class=""center"">7,4</td></tr><tr  class=""row2""><td >24.11.2020 05:15</td><td  class=""center"">7,4</td></tr><tr  class=""row""><td >24.11.2020 05:00</td><td  class=""center"">7,3</td></tr><tr  class=""row2""><td >24.11.2020 04:45</td><td  class=""center"">7,3</td></tr><tr  class=""row""><td >24.11.2020 04:30</td><td  class=""center"">7,3</td></tr><tr  class=""row2""><td >24.11.2020 04:15</td><td  class=""center"">7,3</td></tr><tr  class=""row""><td >24.11.2020 04:00</td><td  class=""center"">7,3</td></tr><tr  class=""row2""><td >24.11.2020 03:45</td><td  class=""center"">7,3</td></tr><tr  class=""row""><td >24.11.2020 03:30</td><td  class=""center"">7,3</td></tr><tr  class=""row2""><td >24.11.2020 03:15</td><td  class=""center"">7,3</td></tr><tr  class=""row""><td >24.11.2020 03:00</td><td  class=""center"">7,3</td></tr><tr  class=""row2""><td >24.11.2020 02:45</td><td  class=""center"">7,3</td></tr><tr  class=""row""><td >24.11.2020 02:30</td><td  class=""center"">7,3</td></tr><tr  class=""row2""><td >24.11.2020 02:15</td><td  class=""center"">7,4</td></tr><tr  class=""row""><td >24.11.2020 02:00</td><td  class=""center"">7,4</td></tr><tr  class=""row2""><td >24.11.2020 01:45</td><td  class=""center"">7,4</td></tr><tr  class=""row""><td >24.11.2020 01:30</td><td  class=""center"">7,4</td></tr><tr  class=""row2""><td >24.11.2020 01:15</td><td  class=""center"">7,3</td></tr><tr  class=""row""><td >24.11.2020 01:00</td><td  class=""center"">7,3</td></tr><tr  class=""row2""><td >24.11.2020 00:45</td><td  class=""center"">7,3</td></tr><tr  class=""row""><td >24.11.2020 00:30</td><td  class=""center"">7,2</td></tr><tr  class=""row2""><td >24.11.2020 00:15</td><td  class=""center"">7,2</td></tr><tr  class=""row""><td >24.11.2020 00:00</td><td  class=""center"">7,2</td></tr><tr  class=""row2""><td >23.11.2020 23:45</td><td  class=""center"">7,1</td></tr><tr  class=""row""><td >23.11.2020 23:30</td><td  class=""center"">7,1</td></tr><tr  class=""row2""><td >23.11.2020 23:15</td><td  class=""center"">7,1</td></tr><tr  class=""row""><td >23.11.2020 23:00</td><td  class=""center"">7,1</td></tr><tr  class=""row2""><td >23.11.2020 22:45</td><td  class=""center"">7,1</td></tr><tr  class=""row""><td >23.11.2020 22:30</td><td  class=""center"">7,1</td></tr><tr  class=""row2""><td >23.11.2020 22:15</td><td  class=""center"">7,1</td></tr><tr  class=""row""><td >23.11.2020 22:00</td><td  class=""center"">7,1</td></tr><tr  class=""row2""><td >23.11.2020 21:45</td><td  class=""center"">7,0</td></tr><tr  class=""row""><td >23.11.2020 21:30</td><td  class=""center"">7,0</td></tr><tr  class=""row2""><td >23.11.2020 21:15</td><td  class=""center"">7,0</td></tr><tr  class=""row""><td >23.11.2020 21:00</td><td  class=""center"">7,0</td></tr><tr  class=""row2""><td >23.11.2020 20:45</td><td  class=""center"">6,9</td></tr><tr  class=""row""><td >23.11.2020 20:30</td><td  class=""center"">6,9</td></tr><tr  class=""row2""><td >23.11.2020 20:15</td><td  class=""center"">6,9</td></tr><tr  class=""row""><td >23.11.2020 20:00</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 19:45</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 19:30</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 19:15</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 19:00</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 18:45</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 18:30</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 18:15</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 18:00</td><td  class=""center"">6,7</td></tr><tr  class=""row2""><td >23.11.2020 17:45</td><td  class=""center"">6,7</td></tr><tr  class=""row""><td >23.11.2020 17:30</td><td  class=""center"">6,7</td></tr><tr  class=""row2""><td >23.11.2020 17:15</td><td  class=""center"">6,7</td></tr><tr  class=""row""><td >23.11.2020 17:00</td><td  class=""center"">6,7</td></tr><tr  class=""row2""><td >23.11.2020 16:45</td><td  class=""center"">6,7</td></tr><tr  class=""row""><td >23.11.2020 16:30</td><td  class=""center"">6,7</td></tr><tr  class=""row2""><td >23.11.2020 16:15</td><td  class=""center"">6,7</td></tr><tr  class=""row""><td >23.11.2020 16:00</td><td  class=""center"">6,7</td></tr><tr  class=""row2""><td >23.11.2020 15:45</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 15:30</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 15:15</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 15:00</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 14:45</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 14:30</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 14:15</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 14:00</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 13:45</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 13:30</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 13:15</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 13:00</td><td  class=""center"">6,9</td></tr><tr  class=""row2""><td >23.11.2020 12:45</td><td  class=""center"">6,9</td></tr><tr  class=""row""><td >23.11.2020 12:30</td><td  class=""center"">6,9</td></tr><tr  class=""row2""><td >23.11.2020 12:15</td><td  class=""center"">6,9</td></tr><tr  class=""row""><td >23.11.2020 12:00</td><td  class=""center"">6,9</td></tr><tr  class=""row2""><td >23.11.2020 11:45</td><td  class=""center"">6,9</td></tr><tr  class=""row""><td >23.11.2020 11:30</td><td  class=""center"">6,9</td></tr><tr  class=""row2""><td >23.11.2020 11:15</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 11:00</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 10:45</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 10:30</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 10:15</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 10:00</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 09:45</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 09:30</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 09:15</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 09:00</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 08:45</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 08:30</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 08:15</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 08:00</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 07:45</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 07:30</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 07:15</td><td  class=""center"">6,9</td></tr><tr  class=""row""><td >23.11.2020 07:00</td><td  class=""center"">6,9</td></tr><tr  class=""row2""><td >23.11.2020 06:45</td><td  class=""center"">6,9</td></tr><tr  class=""row""><td >23.11.2020 06:30</td><td  class=""center"">6,9</td></tr><tr  class=""row2""><td >23.11.2020 06:15</td><td  class=""center"">6,9</td></tr><tr  class=""row""><td >23.11.2020 06:00</td><td  class=""center"">6,9</td></tr><tr  class=""row2""><td >23.11.2020 05:45</td><td  class=""center"">6,9</td></tr><tr  class=""row""><td >23.11.2020 05:30</td><td  class=""center"">6,9</td></tr><tr  class=""row2""><td >23.11.2020 05:15</td><td  class=""center"">6,9</td></tr><tr  class=""row""><td >23.11.2020 05:00</td><td  class=""center"">6,9</td></tr><tr  class=""row2""><td >23.11.2020 04:45</td><td  class=""center"">6,9</td></tr><tr  class=""row""><td >23.11.2020 04:30</td><td  class=""center"">6,9</td></tr><tr  class=""row2""><td >23.11.2020 04:15</td><td  class=""center"">6,9</td></tr><tr  class=""row""><td >23.11.2020 04:00</td><td  class=""center"">6,9</td></tr><tr  class=""row2""><td >23.11.2020 03:45</td><td  class=""center"">6,9</td></tr><tr  class=""row""><td >23.11.2020 03:30</td><td  class=""center"">6,9</td></tr><tr  class=""row2""><td >23.11.2020 03:15</td><td  class=""center"">6,9</td></tr><tr  class=""row""><td >23.11.2020 03:00</td><td  class=""center"">6,9</td></tr><tr  class=""row2""><td >23.11.2020 02:45</td><td  class=""center"">6,9</td></tr><tr  class=""row""><td >23.11.2020 02:30</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 02:15</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 02:00</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 01:45</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 01:30</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 01:15</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 01:00</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 00:45</td><td  class=""center"">6,8</td></tr><tr  class=""row""><td >23.11.2020 00:30</td><td  class=""center"">6,8</td></tr><tr  class=""row2""><td >23.11.2020 00:15</td><td  class=""center"">6,7</td></tr><tr  class=""row""><td >23.11.2020 00:00</td><td  class=""center"">6,8</td></tr></tbody></table></div></div></div>        <div id=""navi_rechts_3c"" class=""col3""><div class=""row""><h3 style=""text-align:right"">Download</h3><div class=""col"">
        <h4>Aktuelle Auswahl herunterladen:</h4>
        <ul>
        <li><a href=""#"" id=""dc_basket"">In den Download-Korb</a></li>
        <li><a href=""#"" id=""dc_download"">Direkter Download</a></li>
        <!--<li>FAQ-Downloadcenter</li>-->
        </ul></div></div><div class=""row""><h3 style=""text-align:right"">Erläuterungen</h3><div class=""col""><h4>Wochendiagramm</h4><p><p>
Darstellung der hochaufgelösten Messwerte der Wassertemperatur für den Zeitraum einer Woche.  
<br/>
<br/>
 Unterhalb der Grafik kann um jeweils eine Woche vor- bzw. zur&uuml;ckgebl&auml;ttert werden.
<br />
<br />
</p></p></div></div><div class=""row""><div class=""col""><h4>Tabelle</h4><p><p>Auflistung der hochaufgelösten Messwerte der Wassertemperatur für den Zeitraum einer Woche.</p>
</p></div></div></div>

        <div id=""footer"">
            <a href=""#top"" title=""zum Seitenanfang""><img alt=""zum Seitenanfang""
                                                                   src=""https://www.gkd.bayern.de/images/symbole/top.gif""
                                                                   width=""12"" height=""12""/></a>
            <br/>
            <hr/>
            <div id=""seitenabschluss"">&copy; Bayerisches Landesamt für Umwelt 2020</div>
        </div>
        <!-- id = content -->
    </div>
    <!-- id = center -->
</div>

<!--<script src=""//ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js""></script>-->
						<script>var burl='https://www.gkd.bayern.de/';
						var shurl='https://www.gkd.bayern.de/files/';
						var wsurl='https://www.gkd.bayern.de/webservices/';
						var lfu_thema = 'gkd';var lfu_rubrik = 'fluesse';var lfu_produkt = 'wassertemperatur';var lfu_lang = 'de';
						window.jQuery || document.write('<script src=""https://www.gkd.bayern.de/js/plugins/jquery-3.3.1.min.js""><\/script>');
						</script>
						<!--[if lt IE 9]><script src=""https://www.gkd.bayern.de/files/js/es5-shim-4.5.9/es5-shim.min.js""></script><![endif]--><script type=""text/javascript"">var _paq = _paq || [];
                        _paq.push(['setSessionCookieTimeout', timeoutInSeconds=0]); _paq.push(['setVisitorCookieTimeout', timeoutInSeconds=7200]); _paq.push(['trackPageView']);
                        _paq.push(['enableLinkTracking']);(function() {var u=""https://www.piwik.bayern.de/piwik/"";_paq.push(['setTrackerUrl', u+'piwik.php']);
                        _paq.push(['setSiteId', 216]); var d=document, g=d.createElement('script'), s=d.getElementsByTagName('script')[0];
                        g.type='text/javascript'; g.async=true; g.defer=true; g.src=u+'piwik.js'; s.parentNode.insertBefore(g,s); })();</script><script src=""https://www.gkd.bayern.de/js/lfu/script.1606233671.js""></script><script src=""https://www.gkd.bayern.de/files/js/jquery-ui-1.12.1/jquery-ui.min.js""></script><script src=""https://www.gkd.bayern.de/files/js/tablesorter/jquery.tablesorter.min.js""></script><script>$(function() {$( ""#dialog-korb"" ).dialog({
  autoOpen: false,
  modal: true,
  height: ""auto"",
  width: $(window).width() >= 600 ? 600 : $(window).width(),
  buttons: {
    ""Zum Download-Korb"": function() {
        window.location = ""https://www.gkd.bayern.de/de/downloadcenter"";
    },
    ""Fenster schließen"": function() {
        $( this ).dialog( ""close"" );
    }
  }
});});var deeplink;$(function() {$( ""#dialog-mail"" ).dialog({
  autoOpen: false,
  modal: true,
  height: ""auto"",
  width: $(window).width() >= 600 ? 600 : $(window).width(),
  closeOnEscape: false,
  open: function(event, ui) { $("".ui-dialog-titlebar-close"").hide(); },
  buttons: {
    ""OK"": function() {
        window.location = deeplink;
    }
  }
});});$(function() {
    var email = $( ""#email_wizard"" ),
    tac = $( ""#tac"" ),
    allFields = $( [] ).add( tac.parent() ).add( email );
    
    $(""#dc_download"").click(function(e) {
        e.preventDefault();
        $( ""#dialog-download"" ).dialog( ""open"" );
    });
    
    $(""#dc_basket"").click(function(e) {
        e.preventDefault();
        $.ajax({
                type: ""POST"",
                url: ""https://www.gkd.bayern.de/de/downloadcenter/add_download"",
                data: {
                    zr: ""individuell"",
                    beginn: ""23.11.2020"",
                    ende: ""24.11.2020"",
                    wertart: ""ezw"",
                    t: '{""16516008"":[""fluesse.wassertemperatur""]}'
                },
                success: function(data){
                    $("".downloadanz"").text(data.overall);
                    $( ""#dialog-korb"" ).dialog( ""open"" );
                }
        });
     });
    
    $( ""#dialog-download"" ).dialog({
      autoOpen: false,
      modal: true,
      height: ""auto"",
      width: $(window).width() >= 600 ? 600 : $(window).width(),
      buttons: {
        ""OK"": function() {
            var valid = true;
            allFields.removeClass( ""ui-state-error"" );
            valid = valid && (email.val() == """" || checkRegexp( email, emailRegex));
            valid = valid && checkChecked( tac);
            var here = this;
             
            if ( valid ) {
                $.ajax({
                    type: ""POST"",
                    url: ""https://www.gkd.bayern.de/de/downloadcenter/enqueue_download"",
                    data: {
                        zr: ""individuell"",
                        beginn: ""23.11.2020"",
                        ende: ""24.11.2020"",
                        wertart: ""ezw"",
                        email: email.val(),
                        t: '{""16516008"":[""fluesse.wassertemperatur""]}'
                    },
                    success: function(data){
                        deeplink = ""https://www.gkd.bayern.de/de/downloadcenter/download?token="" +data.deeplink;
                        $(""#deeplink"").html('<a href=""'+deeplink+'"">'+deeplink+'</a>');                
                        $( here ).dialog( ""close"" );
                        email.val("""");
                        tac.prop('checked', false);
                        $( ""#dialog-mail"" ).dialog( ""open"" );
                    },
                    error: function(data) {
                        if(data.responseText)
                            alert(data.responseText);
                        else
                            alert(""Es ist ein Problem aufgetreten. Bitte versuchen Sie es später erneut."");
                    }
                });
            }
            return valid;
        },
        ""Abbrechen"": function() {
            $( this ).dialog( ""close"" );
        }
      }
    });
    
}); $(function() {
$( ""#beginn"" ).datepicker({
changeMonth: true,
changeYear: true,
dateFormat: ""dd.mm.yy"",
maxDate: ""+0D"",
monthNamesShort: [""Jan"",""Feb"",""Mär"",""Apr"",""Mai"",""Jun"",""Jul"",""Aug"",""Sep"",""Okt"",""Nov"",""Dez""],
prevText: ""<Zurück"",
nextText: ""Vor>"",
showOn: ""button"",
buttonImage: ""https://www.gkd.bayern.de/images/layout/calendar.gif"",
buttonImageOnly: true,
buttonText: ""Datum auswählen"",
onClose: function( selectedDate ) {
	$( ""#ende"" ).datepicker( ""option"", ""minDate"", selectedDate );
}
}).on(""keydown"", function(e){
    if (e.which == 13) {
        $(this).closest(""form"").submit();
    }
});
$( ""#ende"" ).datepicker({
changeMonth: true,
changeYear: true,
dateFormat: ""dd.mm.yy"",
maxDate: ""+0D"",
monthNamesShort: [""Jan"",""Feb"",""Mär"",""Apr"",""Mai"",""Jun"",""Jul"",""Aug"",""Sep"",""Okt"",""Nov"",""Dez""],
prevText: ""<Zurück"",
nextText: ""Vor>"",
showOn: ""button"",
buttonImage: ""https://www.gkd.bayern.de/images/layout/calendar.gif"",
buttonImageOnly: true,
buttonText: ""Datum auswählen"",
onClose: function( selectedDate ) {
	$( ""#beginn"" ).datepicker( ""option"", ""maxDate"", selectedDate );
}
}).on(""keydown"", function(e){
    if (e.which == 13) {
        $(this).closest(""form"").submit();
    }
});
});</script><!-- Matomo Image Tracker-->
			<noscript>
			<img src=""https://www.piwik.bayern.de/piwik/piwik.php?idsite=216"" style=""border:0"" alt="""" />
			</noscript><div id=""dialog-korb"" class=""wizard-dialog"" title=""In den Download-Korb""><span style=""color: green; font-weight: bold;font-size: 1.2em"">Ihre Auswahl wurde erfolgreich in den Download-Korb gelegt.</span></div><div id=""dialog-mail"" class=""wizard-dialog"" title=""Direkter Download""><span style=""color: green; font-weight: bold;font-size: 1.2em"">Ihr Download-Korb wird verarbeitet. Nach erfolgreicher Generierung erhalten Sie den Download-Link per E-Mail zugesandt.<br><br>Den aktuellen Verarbeitungsstatus können Sie unter folgender URL prüfen: <span id=""deeplink""></span></span></div><div id=""dialog-download"" class=""wizard-dialog"" title=""Direkter Download"">
    <label>
    <div style=""padding-bottom: 5px""> (Optional) Bitte geben Sie eine Email-Adresse an, unter der wir Sie über den fertigen Download benachrichtigen können (diese Adresse wird von uns nicht gespeichert)</div>
    <input type=""text"" name=""email"" id=""email_wizard"" value="""" placeholder=""mail@domain.tld"" style=""padding:3px;width: 300px"">  </label>

    <label><input type=""checkbox"" name=""tac"" value=""1"" id=""tac"">   Ich habe die <a href='https://www.gkd.bayern.de/de/impressum' target='_blank'>Nutzungsbedingungen</a> gelesen und bin mit diesen einverstanden.</label>
</div></body>
</html>";

        private static string _levelDataA = @"<!DOCTYPE html>
<html lang=""de"">
<head>
    <meta charset=""utf-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
    <title>Wasserstand: Aktuelle Messwerte München Tierärztl. Hochschule / Schwabinger Bach</title>
    <link href=""https://www.gkd.bayern.de/css/style.css"" rel=""stylesheet"">
    <link rel=""icon"" href=""https://www.gkd.bayern.de/favicon.ico"">
    <link rel=""canonical"" href=""https://www.gkd.bayern.de/de/fluesse/wasserstand/isar/muenchen-tieraerztl-hochschule-16516008/messwerte/tabelle"" />
    <meta name=""description"" content=""""><meta name=""keywords"" content=""Gewässerkundlicher Dienst,Bayern,Messdaten,Gewässerkunde,Download,Abfluss,Grundwasser,Grundwasserstand,Niederschlag,Wassertemperatur,Fluss,See,Gewaesser,Wasserstand,GKD,Gewässerchemie""><meta name=""robots"" content=""index, follow""><link href=""https://www.gkd.bayern.de/files/js/jquery-ui-1.12.1/jquery-ui.min.css"" rel=""stylesheet""><link href=""https://www.gkd.bayern.de/files/js/jquery-ui-1.12.1/jquery-ui.min.css"" rel=""stylesheet""><script src=""https://www.gkd.bayern.de/files/js/jquery-3.3.1/jquery.min.js""></script><script src=""https://www.gkd.bayern.de/files/js/jquery/jquery.maphilight.min.js""></script></head>

<body>
<a title=""#"" name=""top""></a>
<div id=""center"" class=""wide"">
    <div id=""kopf"">
        <div id=""schriftzug"">
            <div id=""navi_meta""><ul><li class=""""><a href=""https://www.gkd.bayern.de/de/"" title=""Startseite"" id=""Startseite"">Startseite</a></li><li class="" append_mobile""><a href=""https://www.gkd.bayern.de/de/kontakt"" title=""Kontakt"" id=""Kontakt"">Kontakt</a></li><li class="" append_mobile""><a href=""https://www.gkd.bayern.de/de/impressum"" title=""Impressum"" id=""Impressum"">Impressum</a></li><li class="" append_mobile""><a href=""https://www.gkd.bayern.de/de/datenschutz"" title=""Datenschutz"" id=""Datenschutz"">Datenschutz</a></li><li class=""""><a href=""http://www.lfu.bayern.de/"" target=""_blank"" id=""LfU-Hauptangebot"">LfU-Hauptangebot</a></li></ul></div>
            <div id=""lfu""><img src=""https://www.gkd.bayern.de/images/layout/schriftzug_l.png"" alt=""Bayerisches Landesamt f&uuml;r Umwelt""></div>
        </div>
        <div id=""kopfgrafik"">Gewässerkundlicher Dienst Bayern</div>
        <div id=""navi_horizontal_container"">
            <div id=""navi_horizontal"">
                <ul><li class=""active""><a href=""https://www.gkd.bayern.de/de/fluesse"" class=""active"" id=""Fl&uuml;sse"">Fl&uuml;sse</a><ul class=""hide""><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/wasserstand"" id=""Wasserstand"">Wasserstand</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/abfluss"" id=""Abfluss"">Abfluss</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur"" id=""Wassertemperatur"">Wassertemperatur</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/schwebstoff"" id=""Schwebstoff"">Schwebstoff</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/chemie"" id=""Chemie"">Chemie</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/biologie"" id=""Biologie"">Biologie</a></li></ul></li><li class=""""><a href=""https://www.gkd.bayern.de/de/seen"" id=""Seen"">Seen</a><ul class=""hide""><li class=""""><a href=""https://www.gkd.bayern.de/de/seen/wasserstand"" id=""Wasserstand"">Wasserstand</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/seen/wassertemperatur"" id=""Wassertemperatur"">Wassertemperatur</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/seen/chemie"" id=""Chemie"">Chemie</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/seen/biologie"" id=""Biologie"">Biologie</a></li></ul></li><li class=""""><a href=""https://www.gkd.bayern.de/de/meteo"" id=""Meteorologie"">Meteorologie</a><ul class=""hide""><li class=""""><a href=""https://www.gkd.bayern.de/de/meteo/niederschlag"" id=""Niederschlag"">Niederschlag</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/meteo/schnee"" id=""Schnee"">Schnee</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/meteo/lufttemperatur"" id=""Lufttemperatur"">Lufttemperatur</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/meteo/luftfeuchte"" id=""Relative Luftfeuchte"">Relative Luftfeuchte</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/meteo/wind"" id=""Wind"">Wind</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/meteo/globalstrahlung"" id=""Globalstrahlung"">Globalstrahlung</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/meteo/luftdruck"" id=""Luftdruck"">Luftdruck</a></li></ul></li><li class=""""><a href=""https://www.gkd.bayern.de/de/grundwasser"" id=""Grundwasser"">Grundwasser</a><ul class=""hide""><li class=""""><a href=""https://www.gkd.bayern.de/de/grundwasser/oberesstockwerk"" id=""Wasserstand oberes Stockwerk"">Wasserstand oberes Stockwerk</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/grundwasser/tieferestockwerke"" id=""Wasserstand tiefere Stockwerke"">Wasserstand tiefere Stockwerke</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/grundwasser/quellschuettung"" id=""Quellsch&uuml;ttung"">Quellsch&uuml;ttung</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/grundwasser/quelltemperatur"" id=""Quelltemperatur"">Quelltemperatur</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/grundwasser/chemie"" id=""Chemie"">Chemie</a></li></ul></li><li class="" float_right""><a href=""https://www.gkd.bayern.de/de/downloadcenter"" id=""downloadcenter"">Downloadcenter (<span class=""downloadanz"">0</span>)</a><ul class=""hide""><li class=""""><a href=""https://www.gkd.bayern.de/de/downloadcenter"" id=""Download-Korb"">Download-Korb</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/downloadcenter/wizard"" id=""Auswahl über Karte"">Auswahl über Karte</a></li></ul></li></ul>            </div>

            <div id=""navi_horizontal_sub"">
                <ul><li class=""active""><a href=""https://www.gkd.bayern.de/de/fluesse/wasserstand"" class=""active"" id=""Wasserstand"">Wasserstand</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/abfluss"" id=""Abfluss"">Abfluss</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur"" id=""Wassertemperatur"">Wassertemperatur</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/schwebstoff"" id=""Schwebstoff"">Schwebstoff</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/chemie"" id=""Chemie"">Chemie</a></li><li class=""""><a href=""https://www.gkd.bayern.de/de/fluesse/biologie"" id=""Biologie"">Biologie</a></li><li> &nbsp;  &nbsp; </li></ul>            </div>
        </div>
    </div>

    <div id=""content"">
        <div id=""surfpfad"">
            <ul>
                <li><a href=""https://www.gkd.bayern.de/de/"">Startseite</a> &gt;</li><li> <a href=""https://www.gkd.bayern.de/de/fluesse"">Fl&uuml;sse</a> &gt;</li><li> <a href=""https://www.gkd.bayern.de/de/fluesse/wasserstand"">Wasserstand</a> &gt;</li><li> <a href=""https://www.gkd.bayern.de/de/fluesse/wasserstand/isar"">Isar</a> &gt;</li><li> <a href=""https://www.gkd.bayern.de/de/fluesse/wasserstand/isar/muenchen-tieraerztl-hochschule-16516008"">München Tierärztl. Hochschule</a> &gt;</li><li> Aktuelle Messwerte</li>
            </ul>

            <div id=""kopfsymbole"">
                <ul style=""padding-left:15px;"">
                    <li><a href=""https://www.gkd.bayern.de/en/rivers/waterlevel/isar/muenchen-tieraerztl-hochschule-16516008/current-values/table""><img alt=""English version"" title=""English version"" src=""https://www.gkd.bayern.de/images/symbole/gb.png"" /></a></li>                </ul>
                <form action=""https://www.gkd.bayern.de/de/search"" method=""get"" name=""searchform"">
                    <input onclick=""if(this.value=='Suchbegriff') this.value='';"" type=""text"" name=""suche"" id=""q""
                           value=""Suchbegriff"" size=""18"" maxlength=""128""/>
                    <input type=""submit"" id=""submit"" value=""&gt;&gt;""/>
                </form>
            </div>
            <!--/UdmComment-->

            <!-- id = surfpfad -->
        </div>
        <!-- Janus Header End -->


        <div id=""navi_links_3c""><div class=""row""><div class=""col"">
<div class=""header"">Wasserstand</div>
<h4 style=""margin: 5px 0 5px 5px"">München Tierärztl. Hochschule / Schwabinger Bach</h4>
<ul><li><a class="""" href=""https://www.gkd.bayern.de/de/fluesse/wasserstand/isar/muenchen-tieraerztl-hochschule-16516008"">Stammdaten / Bild / Karte</a></li>
<li><a class=""active"" href=""https://www.gkd.bayern.de/de/fluesse/wasserstand/isar/muenchen-tieraerztl-hochschule-16516008/messwerte"">Aktuelle Messwerte</a></li>
<li><a class="""" href=""https://www.gkd.bayern.de/de/fluesse/wasserstand/isar/muenchen-tieraerztl-hochschule-16516008/monatswerte"">Monatsgrafik</a></li>
<li><a class="""" href=""https://www.gkd.bayern.de/de/fluesse/wasserstand/isar/muenchen-tieraerztl-hochschule-16516008/jahreswerte"">Jahresgrafik</a></li>
<li><a class="""" href=""https://www.gkd.bayern.de/de/fluesse/wasserstand/isar/muenchen-tieraerztl-hochschule-16516008/gesamtzeitraum"">Gesamtzeitraum</a></li>
<li><a class="""" href=""https://www.gkd.bayern.de/de/fluesse/wasserstand/isar/muenchen-tieraerztl-hochschule-16516008/hauptwerte"">Hauptwerte</a></li>
<li><a class="""" href=""https://www.gkd.bayern.de/de/fluesse/wasserstand/isar/muenchen-tieraerztl-hochschule-16516008/download"">Datendownload</a></li>
</ul><h4 style=""margin: 15px 0 5px 5px"">Ansicht</h4><ul><li><a href=""https://www.gkd.bayern.de/de/fluesse/wasserstand/isar/muenchen-tieraerztl-hochschule-16516008/messwerte"">Diagramm</a></li><li><a href=""https://www.gkd.bayern.de/de/fluesse/wasserstand/isar/muenchen-tieraerztl-hochschule-16516008/messwerte/tabelle"" class=""active"">Tabelle</a></li></ul><h4 style=""margin: 15px 0 5px 5px"">Weitere Messwerte</h4><ul><li><a href=""https://www.gkd.bayern.de/de/fluesse/abfluss/isar/muenchen-tieraerztl-hochschule-16516008/messwerte"">Abfluss</a></li><li><a href=""https://www.gkd.bayern.de/de/fluesse/wasserstand/isar/muenchen-tieraerztl-hochschule-16516008/messwerte"" class=""active"">Wasserstand</a></li><li><a href=""https://www.gkd.bayern.de/de/fluesse/wassertemperatur/isar/muenchen-tieraerztl-hochschule-16516008/messwerte"">Wassertemperatur</a></li></ul></div></div></div>        <div id=""content_3c"" class=""col3""><div class=""row"">
	<div class=""heading""><h1>Aktuelle Messwerte München Tierärztl. Hochschule / Schwabinger Bach</h1></div>
	<div class=""col"">Wasserstand vom 23.11.2020 bis zum  24.11.2020<div style=""margin-top:20px;margin-right:5px;""><form  action=""https://www.gkd.bayern.de/de/fluesse/wasserstand/isar/muenchen-tieraerztl-hochschule-16516008/messwerte/tabelle"" method=""GET""><input type=""hidden"" name=""zr"" value=""woche""><input type=""hidden"" name=""addhr"" value=""hr_w_hw""><table id=""downloadtable"" style=""border:0px;"" cellspacing=""0"" cellpadding=""0"" border=""0""><tr><td>Beginn</td><td><input type=""text"" id=""beginn"" name=""beginn"" class=""tcal"" value=""23.11.2020"" /></td><td>Ende</td><td><input type=""text"" id=""ende"" name=""ende"" class=""tcal"" value=""24.11.2020"" /></td><td><input id=""dlbutton"" type=""button"" name=""dl"" style=""width=20px;background-color:#C0C0C0;background-image:url('https://www.gkd.bayern.de/images/layout/okay.png');background-repeat:no-repeat;background-position:center"" onclick=""this.form.submit();"" /></td></tr></table></form></div><table  class=""tblsort""><caption>Wasserstand vom 23.11.2020 bis zum  24.11.2020</caption><thead><tr><th  data-sorter=""shortDate"">Datum</th><th  class=""center"">Wasserstand [cm]</th></tr></thead><tbody><tr  class=""row""><td >24.11.2020 17:00</td><td  class=""center"">63</td></tr><tr  class=""row2""><td >24.11.2020 16:45</td><td  class=""center"">63</td></tr><tr  class=""row""><td >24.11.2020 16:30</td><td  class=""center"">63</td></tr><tr  class=""row2""><td >24.11.2020 16:15</td><td  class=""center"">63</td></tr><tr  class=""row""><td >24.11.2020 16:00</td><td  class=""center"">63</td></tr><tr  class=""row2""><td >24.11.2020 15:45</td><td  class=""center"">63</td></tr><tr  class=""row""><td >24.11.2020 15:30</td><td  class=""center"">62</td></tr><tr  class=""row2""><td >24.11.2020 15:15</td><td  class=""center"">63</td></tr><tr  class=""row""><td >24.11.2020 15:00</td><td  class=""center"">63</td></tr><tr  class=""row2""><td >24.11.2020 14:45</td><td  class=""center"">65</td></tr><tr  class=""row""><td >24.11.2020 14:30</td><td  class=""center"">65</td></tr><tr  class=""row2""><td >24.11.2020 14:15</td><td  class=""center"">65</td></tr><tr  class=""row""><td >24.11.2020 14:00</td><td  class=""center"">65</td></tr><tr  class=""row2""><td >24.11.2020 13:45</td><td  class=""center"">65</td></tr><tr  class=""row""><td >24.11.2020 13:30</td><td  class=""center"">63</td></tr><tr  class=""row2""><td >24.11.2020 13:15</td><td  class=""center"">64</td></tr><tr  class=""row""><td >24.11.2020 13:00</td><td  class=""center"">63</td></tr><tr  class=""row2""><td >24.11.2020 12:45</td><td  class=""center"">64</td></tr><tr  class=""row""><td >24.11.2020 12:30</td><td  class=""center"">65</td></tr><tr  class=""row2""><td >24.11.2020 12:15</td><td  class=""center"">66</td></tr><tr  class=""row""><td >24.11.2020 12:00</td><td  class=""center"">66</td></tr><tr  class=""row2""><td >24.11.2020 11:45</td><td  class=""center"">66</td></tr><tr  class=""row""><td >24.11.2020 11:30</td><td  class=""center"">66</td></tr><tr  class=""row2""><td >24.11.2020 11:15</td><td  class=""center"">66</td></tr><tr  class=""row""><td >24.11.2020 11:00</td><td  class=""center"">67</td></tr><tr  class=""row2""><td >24.11.2020 10:45</td><td  class=""center"">67</td></tr><tr  class=""row""><td >24.11.2020 10:30</td><td  class=""center"">67</td></tr><tr  class=""row2""><td >24.11.2020 10:15</td><td  class=""center"">68</td></tr><tr  class=""row""><td >24.11.2020 10:00</td><td  class=""center"">68</td></tr><tr  class=""row2""><td >24.11.2020 09:45</td><td  class=""center"">67</td></tr><tr  class=""row""><td >24.11.2020 09:30</td><td  class=""center"">67</td></tr><tr  class=""row2""><td >24.11.2020 09:15</td><td  class=""center"">67</td></tr><tr  class=""row""><td >24.11.2020 09:00</td><td  class=""center"">67</td></tr><tr  class=""row2""><td >24.11.2020 08:45</td><td  class=""center"">67</td></tr><tr  class=""row""><td >24.11.2020 08:30</td><td  class=""center"">67</td></tr><tr  class=""row2""><td >24.11.2020 08:15</td><td  class=""center"">67</td></tr><tr  class=""row""><td >24.11.2020 08:00</td><td  class=""center"">67</td></tr><tr  class=""row2""><td >24.11.2020 07:45</td><td  class=""center"">67</td></tr><tr  class=""row""><td >24.11.2020 07:30</td><td  class=""center"">67</td></tr><tr  class=""row2""><td >24.11.2020 07:15</td><td  class=""center"">67</td></tr><tr  class=""row""><td >24.11.2020 07:00</td><td  class=""center"">68</td></tr><tr  class=""row2""><td >24.11.2020 06:45</td><td  class=""center"">67</td></tr><tr  class=""row""><td >24.11.2020 06:30</td><td  class=""center"">66</td></tr><tr  class=""row2""><td >24.11.2020 06:15</td><td  class=""center"">66</td></tr><tr  class=""row""><td >24.11.2020 06:00</td><td  class=""center"">67</td></tr><tr  class=""row2""><td >24.11.2020 05:45</td><td  class=""center"">67</td></tr><tr  class=""row""><td >24.11.2020 05:30</td><td  class=""center"">66</td></tr><tr  class=""row2""><td >24.11.2020 05:15</td><td  class=""center"">66</td></tr><tr  class=""row""><td >24.11.2020 05:00</td><td  class=""center"">66</td></tr><tr  class=""row2""><td >24.11.2020 04:45</td><td  class=""center"">66</td></tr><tr  class=""row""><td >24.11.2020 04:30</td><td  class=""center"">65</td></tr><tr  class=""row2""><td >24.11.2020 04:15</td><td  class=""center"">66</td></tr><tr  class=""row""><td >24.11.2020 04:00</td><td  class=""center"">65</td></tr><tr  class=""row2""><td >24.11.2020 03:45</td><td  class=""center"">65</td></tr><tr  class=""row""><td >24.11.2020 03:30</td><td  class=""center"">65</td></tr><tr  class=""row2""><td >24.11.2020 03:15</td><td  class=""center"">64</td></tr><tr  class=""row""><td >24.11.2020 03:00</td><td  class=""center"">64</td></tr><tr  class=""row2""><td >24.11.2020 02:45</td><td  class=""center"">65</td></tr><tr  class=""row""><td >24.11.2020 02:30</td><td  class=""center"">64</td></tr><tr  class=""row2""><td >24.11.2020 02:15</td><td  class=""center"">64</td></tr><tr  class=""row""><td >24.11.2020 02:00</td><td  class=""center"">64</td></tr><tr  class=""row2""><td >24.11.2020 01:45</td><td  class=""center"">63</td></tr><tr  class=""row""><td >24.11.2020 01:30</td><td  class=""center"">63</td></tr><tr  class=""row2""><td >24.11.2020 01:15</td><td  class=""center"">63</td></tr><tr  class=""row""><td >24.11.2020 01:00</td><td  class=""center"">64</td></tr><tr  class=""row2""><td >24.11.2020 00:45</td><td  class=""center"">62</td></tr><tr  class=""row""><td >24.11.2020 00:30</td><td  class=""center"">61</td></tr><tr  class=""row2""><td >24.11.2020 00:15</td><td  class=""center"">61</td></tr><tr  class=""row""><td >24.11.2020 00:00</td><td  class=""center"">61</td></tr><tr  class=""row2""><td >23.11.2020 23:45</td><td  class=""center"">61</td></tr><tr  class=""row""><td >23.11.2020 23:30</td><td  class=""center"">61</td></tr><tr  class=""row2""><td >23.11.2020 23:15</td><td  class=""center"">60</td></tr><tr  class=""row""><td >23.11.2020 23:00</td><td  class=""center"">61</td></tr><tr  class=""row2""><td >23.11.2020 22:45</td><td  class=""center"">59</td></tr><tr  class=""row""><td >23.11.2020 22:30</td><td  class=""center"">58</td></tr><tr  class=""row2""><td >23.11.2020 22:15</td><td  class=""center"">58</td></tr><tr  class=""row""><td >23.11.2020 22:00</td><td  class=""center"">58</td></tr><tr  class=""row2""><td >23.11.2020 21:45</td><td  class=""center"">57</td></tr><tr  class=""row""><td >23.11.2020 21:30</td><td  class=""center"">57</td></tr><tr  class=""row2""><td >23.11.2020 21:15</td><td  class=""center"">56</td></tr><tr  class=""row""><td >23.11.2020 21:00</td><td  class=""center"">53</td></tr><tr  class=""row2""><td >23.11.2020 20:45</td><td  class=""center"">52</td></tr><tr  class=""row""><td >23.11.2020 20:30</td><td  class=""center"">52</td></tr><tr  class=""row2""><td >23.11.2020 20:15</td><td  class=""center"">52</td></tr><tr  class=""row""><td >23.11.2020 20:00</td><td  class=""center"">52</td></tr><tr  class=""row2""><td >23.11.2020 19:45</td><td  class=""center"">52</td></tr><tr  class=""row""><td >23.11.2020 19:30</td><td  class=""center"">52</td></tr><tr  class=""row2""><td >23.11.2020 19:15</td><td  class=""center"">52</td></tr><tr  class=""row""><td >23.11.2020 19:00</td><td  class=""center"">52</td></tr><tr  class=""row2""><td >23.11.2020 18:45</td><td  class=""center"">53</td></tr><tr  class=""row""><td >23.11.2020 18:30</td><td  class=""center"">53</td></tr><tr  class=""row2""><td >23.11.2020 18:15</td><td  class=""center"">52</td></tr><tr  class=""row""><td >23.11.2020 18:00</td><td  class=""center"">49</td></tr><tr  class=""row2""><td >23.11.2020 17:45</td><td  class=""center"">49</td></tr><tr  class=""row""><td >23.11.2020 17:30</td><td  class=""center"">48</td></tr><tr  class=""row2""><td >23.11.2020 17:15</td><td  class=""center"">48</td></tr><tr  class=""row""><td >23.11.2020 17:00</td><td  class=""center"">48</td></tr><tr  class=""row2""><td >23.11.2020 16:45</td><td  class=""center"">48</td></tr><tr  class=""row""><td >23.11.2020 16:30</td><td  class=""center"">47</td></tr><tr  class=""row2""><td >23.11.2020 16:15</td><td  class=""center"">46</td></tr><tr  class=""row""><td >23.11.2020 16:00</td><td  class=""center"">46</td></tr><tr  class=""row2""><td >23.11.2020 15:45</td><td  class=""center"">45</td></tr><tr  class=""row""><td >23.11.2020 15:30</td><td  class=""center"">46</td></tr><tr  class=""row2""><td >23.11.2020 15:15</td><td  class=""center"">48</td></tr><tr  class=""row""><td >23.11.2020 15:00</td><td  class=""center"">49</td></tr><tr  class=""row2""><td >23.11.2020 14:45</td><td  class=""center"">50</td></tr><tr  class=""row""><td >23.11.2020 14:30</td><td  class=""center"">49</td></tr><tr  class=""row2""><td >23.11.2020 14:15</td><td  class=""center"">48</td></tr><tr  class=""row""><td >23.11.2020 14:00</td><td  class=""center"">47</td></tr><tr  class=""row2""><td >23.11.2020 13:45</td><td  class=""center"">46</td></tr><tr  class=""row""><td >23.11.2020 13:30</td><td  class=""center"">46</td></tr><tr  class=""row2""><td >23.11.2020 13:15</td><td  class=""center"">47</td></tr><tr  class=""row""><td >23.11.2020 13:00</td><td  class=""center"">48</td></tr><tr  class=""row2""><td >23.11.2020 12:45</td><td  class=""center"">48</td></tr><tr  class=""row""><td >23.11.2020 12:30</td><td  class=""center"">49</td></tr><tr  class=""row2""><td >23.11.2020 12:15</td><td  class=""center"">48</td></tr><tr  class=""row""><td >23.11.2020 12:00</td><td  class=""center"">48</td></tr><tr  class=""row2""><td >23.11.2020 11:45</td><td  class=""center"">47</td></tr><tr  class=""row""><td >23.11.2020 11:30</td><td  class=""center"">47</td></tr><tr  class=""row2""><td >23.11.2020 11:15</td><td  class=""center"">47</td></tr><tr  class=""row""><td >23.11.2020 11:00</td><td  class=""center"">47</td></tr><tr  class=""row2""><td >23.11.2020 10:45</td><td  class=""center"">48</td></tr><tr  class=""row""><td >23.11.2020 10:30</td><td  class=""center"">51</td></tr><tr  class=""row2""><td >23.11.2020 10:15</td><td  class=""center"">50</td></tr><tr  class=""row""><td >23.11.2020 10:00</td><td  class=""center"">49</td></tr><tr  class=""row2""><td >23.11.2020 09:45</td><td  class=""center"">48</td></tr><tr  class=""row""><td >23.11.2020 09:30</td><td  class=""center"">50</td></tr><tr  class=""row2""><td >23.11.2020 09:15</td><td  class=""center"">53</td></tr><tr  class=""row""><td >23.11.2020 09:00</td><td  class=""center"">52</td></tr><tr  class=""row2""><td >23.11.2020 08:45</td><td  class=""center"">48</td></tr><tr  class=""row""><td >23.11.2020 08:30</td><td  class=""center"">49</td></tr><tr  class=""row2""><td >23.11.2020 08:15</td><td  class=""center"">49</td></tr><tr  class=""row""><td >23.11.2020 08:00</td><td  class=""center"">49</td></tr><tr  class=""row2""><td >23.11.2020 07:45</td><td  class=""center"">49</td></tr><tr  class=""row""><td >23.11.2020 07:30</td><td  class=""center"">49</td></tr><tr  class=""row2""><td >23.11.2020 07:15</td><td  class=""center"">49</td></tr><tr  class=""row""><td >23.11.2020 07:00</td><td  class=""center"">50</td></tr><tr  class=""row2""><td >23.11.2020 06:45</td><td  class=""center"">51</td></tr><tr  class=""row""><td >23.11.2020 06:30</td><td  class=""center"">51</td></tr><tr  class=""row2""><td >23.11.2020 06:15</td><td  class=""center"">51</td></tr><tr  class=""row""><td >23.11.2020 06:00</td><td  class=""center"">51</td></tr><tr  class=""row2""><td >23.11.2020 05:45</td><td  class=""center"">50</td></tr><tr  class=""row""><td >23.11.2020 05:30</td><td  class=""center"">50</td></tr><tr  class=""row2""><td >23.11.2020 05:15</td><td  class=""center"">50</td></tr><tr  class=""row""><td >23.11.2020 05:00</td><td  class=""center"">51</td></tr><tr  class=""row2""><td >23.11.2020 04:45</td><td  class=""center"">51</td></tr><tr  class=""row""><td >23.11.2020 04:30</td><td  class=""center"">51</td></tr><tr  class=""row2""><td >23.11.2020 04:15</td><td  class=""center"">51</td></tr><tr  class=""row""><td >23.11.2020 04:00</td><td  class=""center"">51</td></tr><tr  class=""row2""><td >23.11.2020 03:45</td><td  class=""center"">51</td></tr><tr  class=""row""><td >23.11.2020 03:30</td><td  class=""center"">51</td></tr><tr  class=""row2""><td >23.11.2020 03:15</td><td  class=""center"">51</td></tr><tr  class=""row""><td >23.11.2020 03:00</td><td  class=""center"">52</td></tr><tr  class=""row2""><td >23.11.2020 02:45</td><td  class=""center"">51</td></tr><tr  class=""row""><td >23.11.2020 02:30</td><td  class=""center"">51</td></tr><tr  class=""row2""><td >23.11.2020 02:15</td><td  class=""center"">52</td></tr><tr  class=""row""><td >23.11.2020 02:00</td><td  class=""center"">52</td></tr><tr  class=""row2""><td >23.11.2020 01:45</td><td  class=""center"">51</td></tr><tr  class=""row""><td >23.11.2020 01:30</td><td  class=""center"">51</td></tr><tr  class=""row2""><td >23.11.2020 01:15</td><td  class=""center"">51</td></tr><tr  class=""row""><td >23.11.2020 01:00</td><td  class=""center"">51</td></tr><tr  class=""row2""><td >23.11.2020 00:45</td><td  class=""center"">50</td></tr><tr  class=""row""><td >23.11.2020 00:30</td><td  class=""center"">50</td></tr><tr  class=""row2""><td >23.11.2020 00:15</td><td  class=""center"">50</td></tr><tr  class=""row""><td >23.11.2020 00:00</td><td  class=""center"">50</td></tr></tbody></table></div></div></div>        <div id=""navi_rechts_3c"" class=""col3""><div class=""row""><h3 style=""text-align:right"">Download</h3><div class=""col"">
        <h4>Aktuelle Auswahl herunterladen:</h4>
        <ul>
        <li><a href=""#"" id=""dc_basket"">In den Download-Korb</a></li>
        <li><a href=""#"" id=""dc_download"">Direkter Download</a></li>
        <!--<li>FAQ-Downloadcenter</li>-->
        </ul></div></div><div class=""row""><h3 style=""text-align:right"">Erläuterungen</h3><div class=""col""><h4>Wochendiagramm</h4><p><p>Der Wasserstand entspricht nicht der Wassertiefe! Er bezieht sich auf den Pegelnullpunkt, der unterhalb der Gewässersohle liegt. Der Grund hierf&uuml;r ist, 
dass die Sohle in vielen nat&uuml;rlichen Gewässern vor allem bei Hochwasser bewegt und ver&auml;ndert wird und damit als feste Bezugsh&ouml;he für die Wasserstandsmessung nicht geeignet ist.
<p>
In der Grafik sind zusätzlich als horizontale Linien einzelne Hauptwerte eingezeichnet. Der Berechnungszeitraum (betrachtete Zeitspanne) für 

diese statistischen Werte ist im Messstellenmenü unter <b> Hauptwerte</b> angegeben:
<br/>
<br/>
<span style=""font-weight:bold"">
HW
</span>&nbsp;
höchster Wert der betrachteten Zeitspanne.
<br/>
<br />
<span style=""font-weight:bold"">
MW
</span>&nbsp;
Arithmetisches Mittel aller Tageswerte der betrachteten Zeitspanne.
<br/>
<br />
<span style=""font-weight:bold"">
NW
</span>&nbsp;
Niedrigster Wert der betrachteten Zeitspanne.
<br/>
<br /> 
 Unterhalb der Grafik kann um jeweils eine Woche vor- bzw. zur&uuml;ckgebl&auml;ttert werden.
<br /> 
<br />
</p></p></div></div><div class=""row""><div class=""col""><h4>Tabelle</h4><p><p>Auflistung der hochaufgelösten Messwerte des Wasserstandes für den Zeitraum einer Woche.</p></p></div></div></div>

        <div id=""footer"">
            <a href=""#top"" title=""zum Seitenanfang""><img alt=""zum Seitenanfang""
                                                                   src=""https://www.gkd.bayern.de/images/symbole/top.gif""
                                                                   width=""12"" height=""12""/></a>
            <br/>
            <hr/>
            <div id=""seitenabschluss"">&copy; Bayerisches Landesamt für Umwelt 2020</div>
        </div>
        <!-- id = content -->
    </div>
    <!-- id = center -->
</div>

<!--<script src=""//ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js""></script>-->
						<script>var burl='https://www.gkd.bayern.de/';
						var shurl='https://www.gkd.bayern.de/files/';
						var wsurl='https://www.gkd.bayern.de/webservices/';
						var lfu_thema = 'gkd';var lfu_rubrik = 'fluesse';var lfu_produkt = 'wasserstand';var lfu_lang = 'de';
						window.jQuery || document.write('<script src=""https://www.gkd.bayern.de/js/plugins/jquery-3.3.1.min.js""><\/script>');
						</script>
						<!--[if lt IE 9]><script src=""https://www.gkd.bayern.de/files/js/es5-shim-4.5.9/es5-shim.min.js""></script><![endif]--><script type=""text/javascript"">var _paq = _paq || [];
                        _paq.push(['setSessionCookieTimeout', timeoutInSeconds=0]); _paq.push(['setVisitorCookieTimeout', timeoutInSeconds=7200]); _paq.push(['trackPageView']);
                        _paq.push(['enableLinkTracking']);(function() {var u=""https://www.piwik.bayern.de/piwik/"";_paq.push(['setTrackerUrl', u+'piwik.php']);
                        _paq.push(['setSiteId', 216]); var d=document, g=d.createElement('script'), s=d.getElementsByTagName('script')[0];
                        g.type='text/javascript'; g.async=true; g.defer=true; g.src=u+'piwik.js'; s.parentNode.insertBefore(g,s); })();</script><script src=""https://www.gkd.bayern.de/js/lfu/script.1606233855.js""></script><script src=""https://www.gkd.bayern.de/files/js/jquery-ui-1.12.1/jquery-ui.min.js""></script><script src=""https://www.gkd.bayern.de/files/js/tablesorter/jquery.tablesorter.min.js""></script><script>$(function() {$( ""#dialog-korb"" ).dialog({
  autoOpen: false,
  modal: true,
  height: ""auto"",
  width: $(window).width() >= 600 ? 600 : $(window).width(),
  buttons: {
    ""Zum Download-Korb"": function() {
        window.location = ""https://www.gkd.bayern.de/de/downloadcenter"";
    },
    ""Fenster schließen"": function() {
        $( this ).dialog( ""close"" );
    }
  }
});});var deeplink;$(function() {$( ""#dialog-mail"" ).dialog({
  autoOpen: false,
  modal: true,
  height: ""auto"",
  width: $(window).width() >= 600 ? 600 : $(window).width(),
  closeOnEscape: false,
  open: function(event, ui) { $("".ui-dialog-titlebar-close"").hide(); },
  buttons: {
    ""OK"": function() {
        window.location = deeplink;
    }
  }
});});$(function() {
    var email = $( ""#email_wizard"" ),
    tac = $( ""#tac"" ),
    allFields = $( [] ).add( tac.parent() ).add( email );
    
    $(""#dc_download"").click(function(e) {
        e.preventDefault();
        $( ""#dialog-download"" ).dialog( ""open"" );
    });
    
    $(""#dc_basket"").click(function(e) {
        e.preventDefault();
        $.ajax({
                type: ""POST"",
                url: ""https://www.gkd.bayern.de/de/downloadcenter/add_download"",
                data: {
                    zr: ""individuell"",
                    beginn: ""23.11.2020"",
                    ende: ""24.11.2020"",
                    wertart: ""ezw"",
                    t: '{""16516008"":[""fluesse.wasserstand""]}'
                },
                success: function(data){
                    $("".downloadanz"").text(data.overall);
                    $( ""#dialog-korb"" ).dialog( ""open"" );
                }
        });
     });
    
    $( ""#dialog-download"" ).dialog({
      autoOpen: false,
      modal: true,
      height: ""auto"",
      width: $(window).width() >= 600 ? 600 : $(window).width(),
      buttons: {
        ""OK"": function() {
            var valid = true;
            allFields.removeClass( ""ui-state-error"" );
            valid = valid && (email.val() == """" || checkRegexp( email, emailRegex));
            valid = valid && checkChecked( tac);
            var here = this;
             
            if ( valid ) {
                $.ajax({
                    type: ""POST"",
                    url: ""https://www.gkd.bayern.de/de/downloadcenter/enqueue_download"",
                    data: {
                        zr: ""individuell"",
                        beginn: ""23.11.2020"",
                        ende: ""24.11.2020"",
                        wertart: ""ezw"",
                        email: email.val(),
                        t: '{""16516008"":[""fluesse.wasserstand""]}'
                    },
                    success: function(data){
                        deeplink = ""https://www.gkd.bayern.de/de/downloadcenter/download?token="" +data.deeplink;
                        $(""#deeplink"").html('<a href=""'+deeplink+'"">'+deeplink+'</a>');                
                        $( here ).dialog( ""close"" );
                        email.val("""");
                        tac.prop('checked', false);
                        $( ""#dialog-mail"" ).dialog( ""open"" );
                    },
                    error: function(data) {
                        if(data.responseText)
                            alert(data.responseText);
                        else
                            alert(""Es ist ein Problem aufgetreten. Bitte versuchen Sie es später erneut."");
                    }
                });
            }
            return valid;
        },
        ""Abbrechen"": function() {
            $( this ).dialog( ""close"" );
        }
      }
    });
    
}); $(function() {
$( ""#beginn"" ).datepicker({
changeMonth: true,
changeYear: true,
dateFormat: ""dd.mm.yy"",
maxDate: ""+0D"",
monthNamesShort: [""Jan"",""Feb"",""Mär"",""Apr"",""Mai"",""Jun"",""Jul"",""Aug"",""Sep"",""Okt"",""Nov"",""Dez""],
prevText: ""<Zurück"",
nextText: ""Vor>"",
showOn: ""button"",
buttonImage: ""https://www.gkd.bayern.de/images/layout/calendar.gif"",
buttonImageOnly: true,
buttonText: ""Datum auswählen"",
onClose: function( selectedDate ) {
	$( ""#ende"" ).datepicker( ""option"", ""minDate"", selectedDate );
}
}).on(""keydown"", function(e){
    if (e.which == 13) {
        $(this).closest(""form"").submit();
    }
});
$( ""#ende"" ).datepicker({
changeMonth: true,
changeYear: true,
dateFormat: ""dd.mm.yy"",
maxDate: ""+0D"",
monthNamesShort: [""Jan"",""Feb"",""Mär"",""Apr"",""Mai"",""Jun"",""Jul"",""Aug"",""Sep"",""Okt"",""Nov"",""Dez""],
prevText: ""<Zurück"",
nextText: ""Vor>"",
showOn: ""button"",
buttonImage: ""https://www.gkd.bayern.de/images/layout/calendar.gif"",
buttonImageOnly: true,
buttonText: ""Datum auswählen"",
onClose: function( selectedDate ) {
	$( ""#beginn"" ).datepicker( ""option"", ""maxDate"", selectedDate );
}
}).on(""keydown"", function(e){
    if (e.which == 13) {
        $(this).closest(""form"").submit();
    }
});
});</script><!-- Matomo Image Tracker-->
			<noscript>
			<img src=""https://www.piwik.bayern.de/piwik/piwik.php?idsite=216"" style=""border:0"" alt="""" />
			</noscript><div id=""dialog-korb"" class=""wizard-dialog"" title=""In den Download-Korb""><span style=""color: green; font-weight: bold;font-size: 1.2em"">Ihre Auswahl wurde erfolgreich in den Download-Korb gelegt.</span></div><div id=""dialog-mail"" class=""wizard-dialog"" title=""Direkter Download""><span style=""color: green; font-weight: bold;font-size: 1.2em"">Ihr Download-Korb wird verarbeitet. Nach erfolgreicher Generierung erhalten Sie den Download-Link per E-Mail zugesandt.<br><br>Den aktuellen Verarbeitungsstatus können Sie unter folgender URL prüfen: <span id=""deeplink""></span></span></div><div id=""dialog-download"" class=""wizard-dialog"" title=""Direkter Download"">
    <label>
    <div style=""padding-bottom: 5px""> (Optional) Bitte geben Sie eine Email-Adresse an, unter der wir Sie über den fertigen Download benachrichtigen können (diese Adresse wird von uns nicht gespeichert)</div>
    <input type=""text"" name=""email"" id=""email_wizard"" value="""" placeholder=""mail@domain.tld"" style=""padding:3px;width: 300px"">  </label>

    <label><input type=""checkbox"" name=""tac"" value=""1"" id=""tac"">   Ich habe die <a href='https://www.gkd.bayern.de/de/impressum' target='_blank'>Nutzungsbedingungen</a> gelesen und bin mit diesen einverstanden.</label>
</div></body>
</html>";

        #endregion
    }
}
