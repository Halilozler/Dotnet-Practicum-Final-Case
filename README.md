# Final Case(CepList)

## Kurulum

1- Projemizde Veritabanları ihtiyac olduğundan ve bunları kısa yoldan çalıştırmak için Docker yüklü olmalıdır.<br/>
[Docker yükleme linki](https://www.docker.com/)

2- Docker kuruduktan sonra. Projemizde kullanılacak olan MongoDb ve Azure Sql ayağa kaldırıyoruz.

Azure Sql ayağa kaldırma komutu <br/>
`docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=MyPass@word" -e "MSSQL_PID=Developer" -e "MSSQL_USER=SA" -p 1433:1433 -d --name=sql mcr.microsoft.com/azure-sql-edge`

MongoDb ayağa kaldırma komutu<br/>
`docker run -d -p 27017:27017 --name list-mongo mongo:latest`

3- Azure Sql içinde veritabanı tablolarımızı oluşturmamız lazım.(Microsoft SQL Server Management Studio programı için anlatılmıştır) İlk olarak "ListDb" adında bir database oluşturuyoruz. Oluşturduktan sonra [query linki](https://github.com/Halilozler/.Net-Practicum-Final-Case/blob/master/script.sql) linkden queryi indirip mssql server managment aracılığı ile açıyoruz.

Execute solunda bulunan alandan oluşturmuş olduğumuz ListDb seçiyoruz ve Execute basıyoruz.

Artık programı kullanmaya başlayabiliriz.

## Çalıştırma

Proje içinde "Final Case" klasörü içine girdikten sonra `dotnet run` komutunu çalıştırın. <br/>
https://localhost:7198 portundan çalışacaktır

## Veritabanı Şemamız
![sql-schema](https://user-images.githubusercontent.com/45699509/223167157-fb72c2ee-95dc-4a0a-a997-161a7005b107.png)

**Lists**: Genel listemizin özelliklerini tutucak tablomuz.<br/>
**ListItem**: Listemiz içindeki ürünleri tutucak tablomuz. Lists tablosu ile one to many ilişkisi vardır.<br/>
**User**: Kullanıcımızın özelliklerini tutan tablomuz.<br/>
**Genre**: Adet, Kilogram, Gram, Litre... gibi ürün değerlendirmemizi sağlayan değerleri tutan tablomuz.<br/>
**Role**: Kullanıcı rolerini tutan tablomuz.


## Endpointler
Öncelikli olarak projemin genel hatlarını anlatırsam. Bir normal kullanıcı ilk başta ana liste oluşturuabilir. Liste oluşturduktan sonra bu liste içine istediği gibi ürünler ekleyebilir. <br/>
Kullanıcı listeyi istediği gibi tamamlayamaz ilk başta liste içindeki ürünleri tamamladım demelidir. <br/>
Liste içindeki bütün ürünleri tamamlayan kullanıcı liste tamamla dedikten sonra otomatik olarak liste ve içindeki ürünler SQL tarafından silinerek MongoDb tarafına yazılır. <br/>
Burada SQL tarafından yük atılmıştır. Admin tarafı ve kullanıcı tamamladığı listeleri görmek isterse direk olarak mongoDb tarafı ilgelenecektir.

### Admin
***(get) /api/Admin*** -> MongoDb tarafına kaydediğimiz bitmiş listelerin hepsini görür.<br/>

### Item(Ürün)
***(post) /api/Item*** -> Liste id vererek isim, miktar ve genre id vererek liste içine ürün oluştırabiliriz.<br/>
***(put) /api/item/{itemId}*** -> item id vererek ürünümüzü tamamlandıya çekebiliriz. Burada önemli konu Kullanıcının ilgili ürüne erişimi olmalıdır. Başka kullanıcı erişemez.<br/>

### User<br/>
***(post) /api/ser/create*** -> User kayıt olması için name, surname, password ve roleId alarak kayıt olur.<br/>
***(post) /api/user*** -> Sisteme kayıtlı user name ve password vererek giriş yapar. Eğer doğru ise Token döner.<br/>

### Lists<br/>
***(get) /api/lists*** -> Token içindne otomatik olark user Id alınır. O kullanıcıya ait listeler itemleri ile getirilir. <br/>
***(get) /api/lists/search?name=..&catname=..*** -> name ve kategori name alınarak kullanıcıya ait listeler getirilir. <br/>
***(get) /api/lists/completelist/{listId}*** -> Kullanıcı kendisinin tamamladığı listeleri görmek isterse kullanacağı endpointimiz. Burada tamamladığı listeler mongoDb tarafından gelir.<br/>
***(post) /api/lists*** -> Liste oluşturmak için endpointimiz.<br/>
***(put) /api/lists/{id}*** -> belirli bir listeyi güncelemek istersek kullanacağımız endpointimiz.<br/>
***(put) /api/lists/completelist/{listId}*** -> list id alarak ilgili liste eğer şartları sağlıyorsa SQL tarafından silinir MongoDb tarafına geçer.<br/>
***(delete) /api/lists/{listId}*** -> Kullanıcı listesini silebileceği endpoint.<br/>

## Resimler
User Ekleme<br/>
![Screenshot 2023-03-06 at 19 48 24](https://user-images.githubusercontent.com/45699509/223177040-1db4597d-68b5-4885-9415-c85f37d2ead1.png)<br/>
User Giriş yapma<br/>
![Screenshot 2023-03-06 at 19 48 47](https://user-images.githubusercontent.com/45699509/223177066-b8f8e7e3-61f3-453c-83f4-ef3457e4be36.png)<br/>
User giriş yaptığında çıkan ekran<br/>
![Screenshot 2023-03-06 at 19 49 05](https://user-images.githubusercontent.com/45699509/223177082-035093a4-620c-4ee5-8686-b44b698055d7.png)<br/>
Liste oluşturma<br/>
![Screenshot 2023-03-06 at 19 50 06](https://user-images.githubusercontent.com/45699509/223177111-c7d83cd2-57b8-4357-9b8e-68f02ccad16d.png)<br/>
Listeye item ekleme<br/>
![Screenshot 2023-03-06 at 19 50 43](https://user-images.githubusercontent.com/45699509/223177133-2e9080b7-c802-4b7b-98d3-e0f37c638744.png)<br/>
Listeyi görme<br/>
![Screenshot 2023-03-06 at 19 51 28](https://user-images.githubusercontent.com/45699509/223177144-c4cf01e6-e22b-4a93-a951-a60bfa3fe573.png)<br/>
Liste arama <br/>
![Screenshot 2023-03-06 at 19 57 02](https://user-images.githubusercontent.com/45699509/223178457-f584fe2d-b304-485d-b208-db7248bb56b3.png)<br/>
Adminin bütün tamamlanan listeleri görmesi <br/>
![Screenshot 2023-03-06 at 19 58 39](https://user-images.githubusercontent.com/45699509/223178868-13e175f9-871c-4f82-8655-205c8e955749.png)<br/>

Aynı formata kullanıcıda tamamladığı listeleri görebilir.


