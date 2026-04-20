#  AdeptiStore API

## Deskripsi Project
API untuk top-up game, terdapat fitur login, register, produk, dan transaksi.

Selain itu, database yang digunakan terdiri dari 4 tabel yang saling berelasi yaitu:
- users
- games
- products
- transactions

Relasi utama terdapat pada tabel transactions yang menghubungkan users dan products melalui atribut user_id dan product_id sebagai foreign key.
Selain itu, tabel products memiliki relasi dengan tabel games melalui game_id, sehingga setiap produk terkait dengan satu game tertentu.

---

##  Teknologi yang Digunakan
- Bahasa Pemrograman: C#
- Framework: ASP.NET Core Web API
- Database: PostgreSQL
- ORM: Entity Framework Core
- Tools: Visual Studio, pgAdmin

---

##  Cara Instalasi & Menjalankan Project

1. Clone repository ini atau download project
2. Buka project di Visual Studio
3. Pastikan PostgreSQL sudah aktif
4. Atur connection string di file `appsettings.json`:
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=adeptistore;Username=postgres;Password=jatigono123"
  }
}

##  Daftar Endpoint API

| Method | URL                         | Keterangan                          |
|--------|-----------------------------|--------------------------------------|
| POST   | /api/Auth/login             | Login user                           |
| POST   | /api/Auth/register          | Registrasi user                      |
| GET    | /api/Games                  | Mengambil semua data game            |
| GET    | /api/Products               | Mengambil semua data produk          |
| GET    | /api/Transactions           | Mengambil semua transaksi            |
| POST   | /api/Transactions           | Menambahkan transaksi baru           |
| GET    | /api/Transactions/{id}      | Mengambil transaksi berdasarkan ID   |
| PUT    | /api/Transactions/{id}      | Mengupdate transaksi                 |
| DELETE | /api/Transactions/{id}      | Menghapus transaksi                  |
