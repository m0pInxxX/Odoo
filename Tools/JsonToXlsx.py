import pandas as pd
import json

def json_to_xlsx(json_file_path, xlsx_file_path):
    try:
        # Membaca file JSON
        with open(json_file_path, 'r') as file:
            data = json.load(file)
        
        # Mengambil data dari struktur JSON yang benar (Payload.Data)
        if data.get('Payload') and data['Payload'].get('Data'):
            df = pd.DataFrame(data['Payload']['Data'])
        else:
            raise ValueError("Format JSON tidak sesuai dengan yang diharapkan")
        
        # Menyimpan DataFrame ke file Excel
        df.to_excel(xlsx_file_path, index=False)
        print(f"File berhasil dikonversi dan disimpan ke: {xlsx_file_path}")
        
    except FileNotFoundError:
        print("Error: File JSON tidak ditemukan")
    except json.JSONDecodeError:
        print("Error: Format JSON tidak valid")
    except Exception as e:
        print(f"Error: Terjadi kesalahan - {str(e)}")

# Contoh penggunaan
if __name__ == "__main__":
    json_file = "Output.json"  # Sesuaikan dengan nama file JSON Anda
    xlsx_file = "Output.xlsx"  # Nama file Excel output
    json_to_xlsx(json_file, xlsx_file)
