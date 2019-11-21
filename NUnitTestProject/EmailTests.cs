using NUnit.Framework;
using BusinessLayer;
using System;

namespace NUnitTestProject
{
    public class EmailTests
    {
        string tst = "Lorem ipsum dolor sit amet, id amet nec curabitur, quis sit pellentesque malesuada, risus quisque platea eros metus amet pellentesque. Lorem vel phasellus at, sed explicabo, ridiculus vulputate lacus at, eros venenatis cum semper mattis. Augue ligula mauris in, tortor tempus nunc imperdiet laoreet lorem, suspendisse praesent ultricies nam, ac malesuada diam eu ullamcorper, pharetra et cras tristique fermentum ad at. Eu dui massa tellus luctus fusce, lacus vel sodales hymenaeos felis sapien, at nam urna, nec fringilla eros nisl, sapien ac porta. Vel cum pellentesque vel adipiscing volutpat, lacus lacus. Morbi cursus wisi erat amet wisi consequat, ornare turpis pellentesque, aliquet id magna id vitae vitae eros, lacus a, vestibulum id ut a. A nisl lacinia suspendisse mauris diam, vel curabitur mattis curabitur eu mauris vel, mi in magna at vivamus. Etiam eros nunc, sed diam erat id mauris, integer in diam ante in tristique, et facilisi amet nulla eros in. Quisque tellus iaculis maecenas pulvinar, massa commodo lorem in etiam."+
        "Neque aliquet, consequat justo impedit cumque voluptatibus mauris.Leo bibendum et, eu ac euismod elit volutpat, neque pariatur integer vestibulum, sit ac sed nec nec at non, ultrices in nam.Ligula luctus eros donec, arcu nunc blandit molestie nascetur, in per at venenatis non facilis, mi consequat morbi accumsan. Suspendisse vestibulum in sit, nulla eget sed laoreet felis integer nec.Fusce fringilla curabitur est massa, iaculis nec in odio.Nulla ligula felis ea feugiat, amet justo, turpis incididunt massa curabitur penatibus, a aenean neque eu mi. Imperdiet nascetur, tempor scelerisque sapien. Curabitur sit ipsum vel vulputate tellus ac, habitant ut velit wisi, mauris posuere sodales tempor nobis. Purus congue, justo dignissim aliquam, vehicula ipsum, dolor dignissim tristique eget suspendisse nulla a." +
        "Eget semper ac nibh ut sodales. Diam at donec gravida mauris nunc, ipsum rutrum arcu eget a, nunc non vitae fringilla.Ipsum mi libero.Adipiscing mattis odio, consectetuer integer et et, molestiae nulla, nunc amet penatibus sed varius.Porta risus sollicitudin nunc, sit cras facilisis id, ut arcu, posuere hendrerit vestibulum lorem eget. Qui arcu dui ligula purus, metus eget dolor, pellentesque ante culpa vestibulum sed parturient pretium, non pulvinar facilisi ac, id vel aliquet dolor vel nonummy." +
        "Nunc metus massa quisque, duis morbi elit consectetuer nibh dictum est, aliquam dolor.Habitant sodales lobortis ante sapien vitae, consequat dictum vivamus mauris in turpis, massa pharetra turpis donec hac pede. Nec sapien non suspendisse vitae donec, lorem nisl dictum, tortor adipiscing fermentum feugiat molestie, a ligula nibh in eaque, accumsan pede ornare vehicula consequat dui. Bibendum a est ante. Adipiscing amet turpis magna pede adipiscing ut, sed feugiat hac pellentesque hymenaeos, netus arcu voluptas.Tellus lacinia eleifend.Massa tincidunt. Nulla dolor auctor vel mauris vitae, quis suspendisse purus nunc molestie. Sed suscipit, vestibulum tortor posuere arcu id et.Nunc maecenas at lorem varius gravida, vel sit, in id, morbi vitae eget mauris et etiam, arcu et in sit cras. Vel et mattis laoreet wisi porta mauris, amet posuere pulvinar, nonummy praesent sed.Placerat nisl pellentesque diam ante, lobortis lectus dolor." +
        "Amet eros porta erat commodo.Porta ultrices ipsum, quam ut pretium suscipit leo, nonummy vestibulum. Fermentum eget diam nec consectetuer, sodales nam lorem tortor, commodo metus, porttitor felis orci nulla quis mi, sed tristique porttitor posuere bibendum eu.Suspendisse sapien tristique libero pede a, sagittis nunc tempus non elit, mollis praesent, orci ac integer parturient viverra pellentesque augue, laoreet tellus. Ligula a diam tincidunt, in risus.Nec pellentesque." +
        "Vitae mauris, facilisis aenean nisi, adipiscing nulla massa mollis posuere, non volutpat vestibulum primis eros, venenatis etiam quam. In ac id, nonummy nibh sed sem, potenti nunc nunc aliquet proin, velit eget aliquam sit, ullamcorper viverra. Sem cursus tempor fermentum dignissim quas nec, tellus quis vestibulum elit, sem turpis, mauris vehicula hendrerit nullam eros dui, iste ullamcorper etiam ac.Vitae massa cras donec eget auctor. Nullam sed ullamcorper sunt dictum tellus et, integer porttitor cras aliquam, vel amet tempor, nulla gravida.Eu eget litora leo condimentum enim. Dui ut luctus, platea ligula vehicula sed. Et nonummy commodo in libero tempus, diam accumsan metus per, deserunt quam nec arcu quam, sodales est sed id neque justo, tempor sodales consectetuer justo commodo. Odio orci libero in magna, magna neque pellentesque leo gravida vel ante, suspendisse tristique vivamus auctor amet lobortis hymenaeos, quisque molestie egestas morbi in magna, vitae ut pellentesque tellus primis." +
        "Laoreet nulla sit et ultrices nam, ipsum urna, donec nunc nulla purus urna elit, eleifend eget.Arcu vestibulum tristique dignissim vulputate ut eu, ad consectetuer fermentum dui. Varius mauris suscipit volutpat in proin posuere, vestibulum ac.Tellus tempus fermentum ut, erat augue feugiat mauris, elementum placerat sit at luctus.Curabitur morbi, dictum tempus nam, ipsum non praesent pulvinar aenean, tellus vel mauris. Aliquid turpis metus ut sociis interdum platea, a mauris leo, ut rhoncus per cras, mauris dictumst fringilla, pharetra porttitor pede ligula malesuada sodales.Id nullam non sint id.Cursus aliquam eu ut massa placerat sem, justo eu fugit aenean wisi, elit suscipit mauris, labore ut metus nonummy. Hendrerit vitae amet, viverra cras, suspendisse pharetra tortor enim purus eget scelerisque. Luctus aliquam donec urna hac, gravida nam, commodo nisl curabitur cursus amet, semper ad dignissim risus ut pellentesque.At mollis, gravida augue ad porta vitae pellentesque.Ac conubia est." +
        "Cursus consectetuer eleifend habitant mattis a enim, mus sed, occaecat amet.Quam ipsum feugiat praesent posuere augue nulla.Lobortis enim gravida enim at, nunc mauris est.Nam duis vitae pellentesque, pede cras vel in dignissim ante. Nibh cillum deserunt in aliquam mi maecenas, urna lectus id lorem dapibus, nullam ullamcorper, maecenas nunc ante." +
        "Risus sem eros quis lectus, elit aliquet et nullam nunc pulvinar nam, pede pede sem dictum nam, tincidunt vestibulum hendrerit a sagittis vivamus mauris.Dolor dis mauris, mi lectus risus diam neque, donec diam sapien et, non lorem rutrum quos, felis tellus adipiscing semper. Rhoncus porttitor mollis morbi vel at, donec phasellus lacinia. Morbi justo duis pharetra sit nulla pede, sodales iure quas id, amet ipsum, convallis nunc nec eget magna, vel arcu ut sociosqu in. Proin augue mauris interdum. Tincidunt gravida nibh lorem, justo ullamcorper porttitor augue dolor at, semper scelerisque viverra, ipsum diam nec magna posuere pharetra velit." +
        "Sem libero sed.Donec tellus ornare at varius, in arcu morbi nunc vivamus laoreet sit. Dolore nec erat wisi elementum.Pharetra ante curabitur, enim ante sem nunc fusce.Dis arcu nec ut tincidunt sit eleifend, orci dolor luctus nisl aliquam aliquet, a aliquam magna libero eleifend. Ornare tortor mollis lacus, duis elit maecenas libero integer mi, luctus eleifend diam tempor. Cras maecenas interdum, cras ipsum est.";
        [Test]
        public void SenderTest()
        {
            MessageFactory factory = null;
            factory = new EmailFactory("E123456789");

            Message message = factory.GetMessageType();
            Assert.Throws<Exception>(() => message.Sender = "");
            Assert.Throws<Exception>(() => message.Sender = "Paul");
            Assert.Throws<Exception>(() => message.Sender = "12345");
            Assert.Throws<Exception>(() => message.Sender = ".,;'[]");
        }
        [Test]
        public void SubjectTest()
        {
            MessageFactory factory = null;
            factory = new EmailFactory("E123456789");

            Message message = factory.GetMessageType();
            Assert.Throws<Exception>(() => message.Subject = "");
            Assert.Throws<Exception>(() => message.Subject = tst); ;        
        }
        [Test]
        public void BodyTest()
        {
            MessageFactory factory = null;
            factory = new EmailFactory("E123456789");

            Message message = factory.GetMessageType();
            Assert.Throws<Exception>(() => message.Body = "");
            Assert.Throws<Exception>(() => message.Body = tst); ;
        }
    }
}