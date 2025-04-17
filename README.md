
# 📘 Blog Platformu

Blog Platformu, kullanıcıların blog yazıları oluşturup düzenleyebildiği, yorum yapabildiği ve beğeni/olumsuz beğeni sistemiyle etkileşime geçebildiği tam işlevli bir web uygulamasıdır. Modern bir tasarıma, kullanıcı dostu arayüze ve güçlü bir backend mimarisine sahiptir.

> 🚀 Bu proje, **ASP.NET Core MVC**, **Entity Framework Core**, **SQL Server**, **Bootstrap 5** gibi teknolojilerle geliştirilmiştir.

---


## 🚧 Özellikler

- 🧑‍💻 Kullanıcı kimlik doğrulama (Register / Login / Logout)
- 📬 Yazı oluşturma, güncelleme ve silme (CRUD)
- 🖼️ Summernote WYSIWYG editörü ile zengin içerik
- 📎 Blog içeriğine istenilen yere resim ekleme
- 🖼️ Kapak fotoğrafı yükleme
- 🔍 Blog arama özelliği
- 🗂️ Kategoriye göre filtreleme
- ❤️ Beğeni ve olumsuz beğeni sistemi
- 💬 Yorum yapma ve alt yorum (cevap) sistemi
- 📊 Görüntülenme sayacı
- 👤 Profil sayfası (fotoğraf, ad, soyad, sosyal medya vb.)
- 🎨 Responsive ve modern tasarım (Bootstrap + custom CSS)
- 🌙 Sidebar navigasyonu (toggle menü)
- 🗄️ Repository Pattern ve Dependency Injection altyapısı

---

## 🛠️ Kullanılan Teknolojiler

| Katman              | Teknolojiler                                                                 |
|---------------------|------------------------------------------------------------------------------|
| **Backend**         | ASP.NET Core MVC, C#, Entity Framework Core, Identity                        |
| **Frontend**        | Razor Pages, Bootstrap 5, jQuery, Summernote                                 |
| **Veritabanı**      | SQL Server                                                                   |
| **Authentication**  | ASP.NET Identity                                                             |
| **Pattern/Mimari**  | Repository Pattern, Dependency Injection, SOLID Prensipleri                  |
| **Yorum/Beğeni**    | Custom relational models with foreign key navigation                         |
| **Hosting (local)** | IIS Express / Kestrel (Visual Studio)                                        |

---

## 🔧 Kurulum Talimatları

### 1. Repoyu Klonla
```bash
git clone https://github.com/kullaniciadi/BlogPlatformu.git
```

### 2. Veritabanı Bağlantısı
`appsettings.json` dosyasındaki connection string’i kendi veritabanınıza göre güncelleyin:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=BlogDb;Trusted_Connection=True;"
}
```

### 3. Migration ve Veritabanı Oluşturma
```bash
dotnet ef database update
```

### 4. Projeyi Çalıştır
Visual Studio ile açıp `Ctrl + F5` ile çalıştırın veya:
```bash
dotnet run
```

---

## 👤 Geliştirici

**Yiğit Seyyid Uçaroğlu**  
📧 Yigitucaroglu@gmail.com 
🌐 www.linkedin.com/in/yiğit-seyyid-uçaroğlu


---


