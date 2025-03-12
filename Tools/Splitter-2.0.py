import pandas as pd
import math
import os
from openpyxl import load_workbook
from openpyxl.utils import get_column_letter

# Baca file Excel dengan dtype=str untuk menghindari konversi otomatis
file_path = 'D:/outputFeb2025.xlsx'
df = pd.read_excel(file_path, dtype=str)

# Pilih hanya kolom yang dibutuhkan
# Ambil refrence AQ sebagai No IV dan TaxInvoiceNumber (kolom N) sebagai No FP
cols_to_extract = ['refrence AQ', 'TaxInvoiceNumber']
if set(cols_to_extract).issubset(df.columns):
    df_selected = df[cols_to_extract].copy()
    # Ganti nama kolom
    df_selected.columns = ['No IV', 'No FP']
else:
    # Cek nama kolom yang tersedia
    if 'refrence AQ' not in df.columns:
        print("Kolom 'refrence AQ' tidak ditemukan. Berikut daftar kolom yang tersedia:")
        for i, col in enumerate(df.columns):
            print(f"Kolom {get_column_letter(i+1)}: {col}")
    if 'TaxInvoiceNumber' not in df.columns:
        print("Kolom 'TaxInvoiceNumber' tidak ditemukan.")
    # Gunakan pendekatan alternatif dengan indeks kolom
    print("Mencoba menggunakan indeks kolom...")
    # AQ adalah kolom ke-43 (indeks 42), N adalah kolom ke-14 (indeks 13)
    df_selected = df.iloc[:, [42, 13]].copy()
    df_selected.columns = ['No IV', 'No FP']

# Filter baris yang kosong
df_selected = df_selected.replace('', pd.NA)  # Konversi string kosong ke NA
df_selected = df_selected.dropna()  # Hapus baris dengan NA

# Hitung jumlah file yang akan dibuat
baris_per_file = 500
total_baris = len(df_selected)
jumlah_file = math.ceil(total_baris / baris_per_file)

# Pisahkan data menjadi beberapa file
for i in range(jumlah_file):
    # Tentukan index awal dan akhir untuk setiap chunk
    start_idx = i * baris_per_file
    end_idx = min((i + 1) * baris_per_file, total_baris)
    
    # Ambil chunk data
    df_chunk = df_selected.iloc[start_idx:end_idx]
    
    # Buat nama file baru
    output_file = f'Report_NoIV_NoFP_{i+1}.xlsx'
    
    # Simpan ke file Excel baru
    df_chunk.to_excel(output_file, index=False)
    
    # Format file Excel yang baru dibuat
    wb = load_workbook(output_file)
    ws = wb.active
    
    # Format kolom
    for column in ws.columns:
        max_length = 0
        column_letter = get_column_letter(column[0].column)
        
        for cell in column:
            try:
                if len(str(cell.value)) > max_length:
                    max_length = len(str(cell.value))
            except:
                pass
        
        adjusted_width = min(max_length + 2, 50)
        ws.column_dimensions[column_letter].width = adjusted_width
    
    # Format header
    for cell in ws[1]:
        cell.style = 'Headline 3'
    
    wb.save(output_file)
    print(f'File {output_file} telah dibuat dengan {len(df_chunk)} baris')

print(f'\nTotal {jumlah_file} file telah dibuat')