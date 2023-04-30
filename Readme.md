~~ Projeyi test etmek için ana dizinde docker-compose up çalıştırmanız yeterlidir.
Ardından http://localhost:8000/swagger/index.html üzerinden testlerinizi yapabilirsiniz.

# Case 1
------------------------

### Algoritma:
- 5 haneli bir başlangıç değerine daha önce üretilmiş olan code sayısını ekleyerek başlıyoruz. Elde ettiğimiz int değerini Skip32 algoritmasını kullanarak 10 byte'lık bir key yardımı ile şifreliyoruz. Çıkan int değerini 22 tabanına çeviriyoruz. Çevirme işlemine başlarken Skip32'den elde ettiğimiz int değeri 0dan küçük mü bakıyoruz sıfırdan küçükse çevirme algoritması sonunda bulunan diğer karakterden değeri en büyük olanı bulup son hane olarak ekliyoruz. Değilse en küçüğü ekliyoruz. Çıkan sonuç 8 karakter değilse başına 23 karakterden ayırdığımız 1 karakteri (Ben 'A' karakterini kullandım) ekliyoruz. Decode işlemi içinde 'A' ları trimliyoruz. Signed karakteri çıkarıyoruz. Base22den tekrar int değere çevirip signed karakter varsa -1 le çarpıyoruz. Ardından Skip32 decrpt işlemi yapıp üretilmiş kod sayımızla karşılaştırıyoruz.
### Notlar:
- Code generation işlemi için Skip32 Encryption algoritmasını kullandım. Daha güvenlikli olan algoritmalar çoğunlukla 128,256,512 bitlik blok şifreleme kullanıyorlar fakat benden beklenen 23 tabanında 8 haneli bir değer olacağı için bana 32 bitlik bloklar kullanan bir algoritma gerekiyordu ve Skip32yi kullanmaya karar verdim.
- Skip32 algoritmasının kodun içinde gömülü olmasının sebebi artık nuget paketi olarak kullanılamaması ve sadece source kod aracılığı ile ulaşılabilmesi. bkn: https://github.com/eleven41/Eleven41.Skip32
- Şifreleme için ilk olarak unique zaman damgası kullanmak ve bu zamanı kontrol edilen zamanla karşılaştırmayı düşündüm. Unique olabilmesi için Datetime'ın tick değişkenini kullanmalıydım fakat hem kontrol aşamasında daha üretilmemiş değerlerin valid olarak atanabileceğini hem de 8 haneli base23 bir değeri oluşturabilmek için kullandığım int değerini aşacağı için bu yöntemden vazgeçtim. Böylece secret bir 5 haneli değeri başlangıç alan ve kod ürettikçe artan (uniqueliği sağlaması için) bir int değer kullandım. docker-compose içerisindeki değişkenleri tutarak istediğimiz zaman ekstra kod üretebileceğiz ve daha önceki üretim sayısını bildiğimiz için hepsi unique olacak.
- Programı kolay test edebilmek için her uygulama her down olduğunda üretilen 0 kod varmış gibi kullandım. Bu kod sayısı herhangi bir yerde saklanabilir. Şimdilik her session başında 0 alıp ürettikçe artar şekilde ayarladım. 


# Case 2
------------------------

### Algoritma:
- Gelen listedeki ilk değeri silip kalan değerlerin sol üst köşesinin y değerini  10'un katlarına yuvarlayıp sıralıyoruz,Yuvarlanmış y değerleri aynı olanları da x değerine göre sıralıyoruz. Ardından listeyi dönmeye başlıyoruz ve yine sol üst köşenin yakınlığına göre listeye ayrı olarak veya önceki ile beraber ekleyip oluşan listeyi dönüyoruz.
### Notlar:
- Burada hardcoded değerler kullandım bunlar gelen listenin elemanlarının standart sapması olarak da belirlenebilirdi fakat elimde tek input olduğu için gerekli mi bilemedim ve hardcoded yaptım.
