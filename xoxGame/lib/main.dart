import 'package:flutter/material.dart';
void main() => runApp(App());
class App extends StatelessWidget {
 //StatelessWidget sınıfını uygulamamın kök sınıfına miras aktardım. SatelessWidget text ve benzeri, yani statik şeyleri kapsayan sınıftır.
 @override
 Widget build(BuildContext context) {
 return MaterialApp(
 title: 'Flutter Demo',
 theme: ThemeData(
 //tema stil ve ayarlarını burada yapıyorum. Ben örnek olması için primarySwatch'e renk verdim bu sayede dialogAlert'deki butonu kırmızı yaptım.
 primarySwatch: Colors.red),
 home: HomePage());
 }
}
class HomePage extends StatefulWidget {
 //buradaki widget ise Stateless'in aksine dinamik araçları kullanmaya yarar.
 @override
 State<StatefulWidget> createState() {
 return _HomePageState();
 }
}
class _HomePageState extends State<HomePage> {
 List<List> _xoxMatris;
 //burada bir matris açtım list veri tipinde tuttum. Bunun sebebi sonu önceden belirtilmesi gerekmeyen bir veri tipi olması.
 // eğer sonradan bu veri ile oynamam gerekirse list tipi işimi hızlandırabilir.
 _HomePageState() {
 _bosMatris();
 }
 _bosMatris() //uygulama başladığı zaman ve tur tekrar başlayacağı zaman bu matris fonksiyonu sayesinde temiz bir ızgara oluşturacak.
 //Bu fonksiyonda, uygulamadaki ızgaranın 3 satır ve 3 sütunu olacağı için matrisin uzunlugunu 3 yaptım
 //çift döngü kullanarak 2 boyutlu matris oluşturdum ve içerisinde döndüm
 //bu işlemin sonucunda bütün ızgaraları boş string ile doldurdum.
 {
 _xoxMatris = List<List>(3);
 for (var i = 0; i < _xoxMatris.length; i++) {
 _xoxMatris[i] = List(3);
 for (var j = 0; j < _xoxMatris[i].length; j++) {
 _xoxMatris[i][j] = ' ';
 }
 }
 }
 @override
 Widget build(BuildContext context)
 //build widget'i iskeleti içine yerleştireceğimiz gövde.
 {
 return Scaffold //uygulamamızın iskeleti.
 (
 backgroundColor: Colors.deepPurple,
 //iskelenin arkaplan rengini verdim
 appBar: AppBar(
 title: Text('XOX Oyunu'), //üst Bar'a text ekledim
 backgroundColor: Colors.purple[900],
 //Üst bar'a arkaplan rengi verdim.
 ),
 body: Center(
 child: Column(
 //gövdeyi dikey hizaladım
 mainAxisAlignment: MainAxisAlignment.spaceEvenly,
 //Izgaraları dikeyde aralarında eşit boşluk olacak şekilde hizaladım.
 children: [
 Row(
 //ilk ızgara satırı
mainAxisAlignment: MainAxisAlignment.spaceEvenly,
//Izgaradaki ilk satırı yatayda eşit boşluk olacak şekilde hizaladım.
 children: [
 //bunlar ızgarada ilk satırdaki 3 hücreyi temsil ediyor.
_izgaraHiza(0, 0),
 _izgaraHiza(0, 1),
 _izgaraHiza(0, 2),
 ],
 ),
 Row(
 //ikinci ızgara satırı
mainAxisAlignment: MainAxisAlignment.spaceEvenly,
//Izgaradaki ikinci satırı yatayda eşit boşluk olacak şekilde hizaladım.
 children: [
 //bunlar ızgarada ikinci satırdaki 3 hücreyi temsil ediyor.
_izgaraHiza(1, 0),
 _izgaraHiza(1, 1),
 _izgaraHiza(1, 2),
 ],
 ),
 Row(
 //üçüncü ızgara satırı
mainAxisAlignment: MainAxisAlignment.spaceEvenly,
//Izgaradaki üçüncü satırı yatayda eşit boşluk olacak şekilde hizaladım.
 children: [
 //bunlar ızgarada üçüncü satırdaki 3 hücreyi temsil ediyor.
_izgaraHiza(2, 0),
 _izgaraHiza(2, 1),
 _izgaraHiza(2, 2),
 ],
 ),
 ],
 ),
 ));
 }
 String _sonOynayan = 'O';
  _izgaraHiza(int i, int j) //
 {
 return GestureDetector //GestureDetector widget'ını tıklama için kullandım.
 (
 onTap: () {
 _hucredekiChar(i, j);
 if (_kazananDurum(i, j))
 //Eğer _kazananDurum fonksiyonundan true değer döndüyse son hücreye koyulan Char değeri
 // dialogAlert'a gönderilir.
 {
 _bildiri(_xoxMatris[i][j]);
 } else if (_beraberlikDurum())
 //Eğer bütün ızgaralar dolduysa ve hala _kazananDurum fonksiyonundan true değer dönmediyse_
 // beraberlikDurum fonksiyonunun true değeri döner ve dialogAlert fonksiyonuna ulaşılır.
 {
 _bildiri(null);
 }
 },
 child: Container(
 width: 120.0,
 height: 120.0,
 //Hücrelerin genişliğini ve yüksekliğini 120.0flout olarak ayarladım.
 decoration: BoxDecoration(
 shape: BoxShape.rectangle,
 //hücrelere kare şeklini verdim.
 border: Border.all(color: Colors.white, width: 4.0),
 //hücrelere beyaz ve 4.0f kalınlığında çerçeve verdim.
 ),
 child: Center(
 child: Text(
 _xoxMatris[i][j],
 style: TextStyle(fontSize: 92.0, color: Colors.white),
 //Hücrelerdeki harflerin rengini ve boyutunu ayarladım.
 ),
 ),
 ),
 );
 }
 _hucredekiChar(int i, int j) {
 setState(() {
 if (_xoxMatris[i][j] == ' ')
 //Eğer seçilen ızgara boş ise
 {
 if (_sonOynayan == 'O')
 _xoxMatris[i][j] = 'X';
 //_sonOynayan "O" ise seçilen ızgaraya "X" değeri yerleşir.
 else
 _xoxMatris[i][j] = 'O';
 //_sonOynayan "X" ise seçilen ızgaraya "O" değeri yerleşir.
 _sonOynayan = _xoxMatris[i][j];
 //ve _sonOynayan değişkenine son oynanan Char verilir.
 }
 });
 }
 _beraberlikDurum() //Beraberlik durumunu kontrol edecek fonksiyon.
 {
 var beraberlikDurum = true;
 //"beraberlikDurum" isminde bir değişken açtım ve true değerini verdim.
 _xoxMatris.forEach((i) {
 i.forEach((j) {
 if (j == ' ') beraberlikDurum = false;
 //Matris'in içerisinde foreach ile döndüm ve boş ızgara var ise "beraberlikDurum" değişkenini false yaptım.
 //bu sayede fonksiyon çağrıldıgında; eğer boş bir ızgara kalmadı ise "beraberlikDurum" değişkeni ilk hali olan true ile
 //dönecek ve "beraberlik" mesajı gösterilecek.
 });
 });
 return beraberlikDurum;
 }
 _kazananDurum(int x, int y) //Kazanma durumunu kontrol edecek fonksiyon.
 {
 var dikey = 0, yatay = 0, capraz = 0, tersCapraz = 0;
 var n = _xoxMatris.length - 1;
 var oyuncu = _xoxMatris[x][y];
 for (int i = 0; i < _xoxMatris.length; i++) {
 if (_xoxMatris[x][i] == oyuncu) dikey++;
 //Burada dikey eksendeki harfleri kontrol ediyorum örneğin; 0-0, 0-1, 0-2 indexleri aynı ise "dikey" değişkenine +1 ekliyorum
 //döngü her döndüğünde i(sırasıyla 0-1-2 indexli değerler) ile player(y) eşit ise 3 döngünün sonunda dikey değişkeninin değeri toplam 3 artıyor.
 if (_xoxMatris[i][y] == oyuncu) yatay++;
 //Burada yatay eksendeki harfleri kontrol ediyorum örneğin; 0-0, 1-0, 2-0 indexleri aynı ise "yatay" değişkenine +1 ekliyorum
 //döngü her döndüğünde i(sırasıyla 0-1-2 indexli değerler) ile player(x) eşit ise 3 döngünün sonunda yatay değişkeninin değeri toplam 3 artıyor.
 if (_xoxMatris[i][i] == oyuncu) capraz++;
 //Burada çapraz eksendeki harfleri kontrol ediyorum örneğin; 0-0, 1-1, 2-2 indexleri aynı ise "capraz" değişkenine +1 ekliyorum
 //döngü her döndüğünde i(sırasıyla 0-1-2 indexli değerler) ile player(x-y) eşit ise 3 döngünün sonunda capraz değişkeninin değeri toplam 3 olur.
 if (_xoxMatris[i][n - i] == oyuncu) tersCapraz++;
 //Burada tersine çapraz eksendeki harfleri kontrol ediyorum örneğin; 0-2, 1- 1, 2-0 indexleri aynı ise "tersCapraz" değişkenine +1 ekliyorum.
 //döngü her döndüğünde i(sırasıyla 0-1-2 indexli değerler) ve n-i(sırasıyla 2-1-0 indexli değerler) ile player(x-y) eşit ise 3 döngünün sonunda tersCapraz değişkeninin değeri toplam 3 olur.
 }
 if (yatay == n + 1 ||
 dikey == n + 1 ||
 capraz == n + 1 ||
 tersCapraz == n + 1)
 //eksenlerden herhangi aynı değerlerle dolunca yani herhangi birinin değişkeni nihayet 3 oldu ise bu fonksiyondan true değer döner.
 //aksi durumda bu fonksiyondan false değeri döner ve bu fonksiyondan ya da beraberlik fonksiyonundan true değeri dönderilene kadar kontrol devam eder.
 {
 return true;
 }
 return false;
 }
 _bildiri(String kazanan)
 //_kazananDurum ve _berabereDurum fonksiyonlarının dönderdiği değerlere bu fonksindan ulaştım.
 // Ayrıca burada alert ekranında gösterilecek string'i de oluşturdum.
 {
 String bildiriMetni;
 if (kazanan == null)
 //eğer _berabereDurum fonkisyonundan dönen değer geldiyse yani dönen değer 'NULL' ise alert ekranında bu string yazacak.
 {
 bildiriMetni = 'BERABERE';
 } else
 //aksi durumda _kazananDurum fonksiyonundan son tıklanan hücre alınır(_xoxMatris[i][j]) ve
 //kazanan değişkenine yüklenir. ve alert ekranında bu string yazılır.
 {
 bildiriMetni = 'KAZANAN $kazanan';
 }
 //Burada da alert için paket oluşturmak yerine flutter'ın sağlamış oldugu bir widget kullandım.
 showDialog(
 context: context,
 builder: (context) {
 return AlertDialog(
 title: Text('OYUN BİTTİ'),
 //Alert'a başlık verdim.
 content: Text(bildiriMetni),
 //Alert'ın metin kısmına _bildiri'dan gelen string'i bastırdım.
 actions: <Widget>[
 FlatButton
 //FlatButton sadece yazı olan, arkasında çerçeve olmayan buton!
 (
 child: Text('TEKRAR'),
 onPressed: () {
 Navigator.of(context).pop();
 setState(() {
 _bosMatris();
//Butona tıklandığında _bosMatris fonksiyonunu çağırdım, bu şekilde uygulamadaki
 // hücreler temizlenecek ve uygulama başa saracak.
 });
 },
 )
 ],
 );
 });
 }
}