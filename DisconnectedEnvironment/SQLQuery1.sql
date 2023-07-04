create database disconnected_envi

use disconnected_envi

CREATE TABLE prodi (
    id_prodi varchar(50) PRIMARY KEY,
    nama_prodi varchar(100)
);

CREATE TABLE mahasiswa (
    nim varchar(20) PRIMARY KEY,
    nama_mahasiswa varchar(100),
    jenis_kelamin char(1),
    alamat varchar(200),
    tgl_lahir varchar(10),
    id_prodi varchar(50),
    FOREIGN KEY (id_prodi) REFERENCES prodi(id_prodi)
);

CREATE TABLE status_mahasiswa (
    id_status varchar(10) PRIMARY KEY,
    nim varchar(20),
    status_mahasiswa varchar(50),
    tahun_masuk varchar(4),
    FOREIGN KEY (nim) REFERENCES mahasiswa(nim)
);