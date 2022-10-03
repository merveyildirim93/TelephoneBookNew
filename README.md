# TelephoneBook
 Bu uygulama bir rehber niteliği taşımaktadır. Kişi ve kişilerin iletişim bilgilerini kayıt altına almanıza olanak sağlamasının yanı sıra konum bazlı raporlama yapmanıza da olanak sağlamaktadır. Uygulama .NetCore ile yazılmış olup raporlama için RabbitMq, Pdf Converter için ise IronPdf kullanılmıştır.
 
Kişi İşlemleri
---
Kişi işlemleri kapsamında listeleme, detay görüntüleme, ekleme ve silme işlemleri gerçekleştirilmiştir.

Listeleme: /Person/Index altında bulunan tabloda Kişinin adı, soyadı ve firma bilgileri listelenir. Bu listeye erişmek için herhangi bir parametreye ihtiyaç duyulmaz.

Ekleme: Veritabanına yeni bir kişi eklemek için ajax ile işlem yapılmış olup parametre olarak modelin kendisi gönderilmiştir.

Silme: Silme işleminde ise parametre olarak "id" gönderilmektedir. Eğer gönderilen id ile daha önce iletişim bilgisi eklenmiş ise bu kaydın silinmesine izin verilmemektedir. Eğer herhangi bir iletişim bilgisi bulunmuyorsa veritabanında eşleşen id bulunarak "Deleted" alanı true olarak değiştirilmiştir.

İletişim Bilgisi İşlemleri
--
İletişim bilgisi işlemleri kapsamında listeleme, detay görüntüleme, ekleme ve silme işlemleri gerçekleştirilmiştir.

Listeleme: /ContactInfo/Index altında bulunan tabloda Kişinin idsi, telefon numarası, mail adresi ve konum bilgileri listelenir. Bu listeye erişmek için "personId" parametresine ihtiyaç duyulmaktadır. Gönderilen id ile daha önce kayıt edilmiş iletişim bilgileri bulunuyorsa bu sayfada görüntülenir.

Ekleme: Veritabanına yeni bir kişi eklemek için ajax ile işlem yapılmış olup parametre olarak modelin kendisi gönderilmiştir.

Silme: Silme işleminde ise parametre olarak "id" gönderilmektedir. Veritabanında eşleşen id bulunarak "Deleted" alanı true olarak değiştirilmiştir. 

Raporlama
--
Raporlama işlemi sistem tarafından otomatik olarak gerçekleştirilmektedir. Kullanıcı "Rapor Al" butonuna tıkladığı zaman bu istek RabbitMq kullanılarak oluşturulmuş olan kuyruğa alınarak sırası geldiğinde işleme alınacaktır. Sistemden rapor almak için herhangi bir parametreye ihtiyaç duyulmamaktadır. Oluşturulmuş olan raporlar sistemde eşsiz bir ReportName ile fiziksel olarak tutulmakta olup listeleme esnasında eğer bu ReportName ile eşleşen dosya bulunuyorsa rapor durumu "Tamamlandı" bulunmuyorsa "Hazırlanıyor" olarak görülmektedir.
