import pandas as pd
import json
import tkinter as tk
from tkinter import filedialog, messagebox
from tkinter import ttk
import os

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
        return True, f"File berhasil dikonversi dan disimpan ke: {xlsx_file_path}"
        
    except FileNotFoundError:
        return False, "Error: File JSON tidak ditemukan"
    except json.JSONDecodeError:
        return False, "Error: Format JSON tidak valid"
    except Exception as e:
        return False, f"Error: Terjadi kesalahan - {str(e)}"

class JsonToXlsxApp:
    def __init__(self, root):
        self.root = root
        self.root.title("JSON ke XLSX Converter")
        self.root.geometry("600x400")  # Diperbesar untuk area log
        self.root.resizable(False, False)
        
        # Variabel untuk menyimpan path file
        self.json_file_path = tk.StringVar()
        self.xlsx_file_path = tk.StringVar()
        
        # Membuat frame utama
        main_frame = ttk.Frame(root, padding="20")
        main_frame.pack(fill="both", expand=True)
        
        # Judul aplikasi
        ttk.Label(main_frame, text="Konversi File JSON ke XLSX by Mahendra", font=("Arial", 16, "bold")).pack(pady=10)
        
        # Frame untuk input file JSON
        json_frame = ttk.Frame(main_frame)
        json_frame.pack(fill="x", pady=5)
        ttk.Label(json_frame, text="File JSON/TXT:").pack(side="left", padx=5)
        ttk.Entry(json_frame, textvariable=self.json_file_path, width=50).pack(side="left", padx=5)
        ttk.Button(json_frame, text="Pilih File", command=self.browse_json_file).pack(side="left")
        
        # Frame untuk output file XLSX
        xlsx_frame = ttk.Frame(main_frame)
        xlsx_frame.pack(fill="x", pady=5)
        ttk.Label(xlsx_frame, text="File XLSX:").pack(side="left", padx=5)
        ttk.Entry(xlsx_frame, textvariable=self.xlsx_file_path, width=50).pack(side="left", padx=5)
        ttk.Button(xlsx_frame, text="Pilih File", command=self.browse_xlsx_file).pack(side="left")
        
        # Tombol konversi
        ttk.Button(main_frame, text="Konversi", command=self.convert_file, style="Accent.TButton").pack(pady=10)
        
        # Area log
        log_frame = ttk.LabelFrame(main_frame, text="Log")
        log_frame.pack(fill="both", expand=True, pady=10)
        
        self.log_text = tk.Text(log_frame, height=8, width=70, wrap="word")
        self.log_text.pack(fill="both", expand=True, padx=5, pady=5)
        
        # Scrollbar untuk area log
        scrollbar = ttk.Scrollbar(self.log_text, command=self.log_text.yview)
        scrollbar.pack(side="right", fill="y")
        self.log_text.config(yscrollcommand=scrollbar.set)
        
        # Status bar
        self.status_var = tk.StringVar()
        self.status_var.set("Siap untuk konversi")
        status_bar = ttk.Label(main_frame, textvariable=self.status_var, relief="sunken", anchor="w")
        status_bar.pack(fill="x", side="bottom", pady=5)
        
        # Styling
        style = ttk.Style()
        style.configure("Accent.TButton", font=("Arial", 11), background="#007bff")
        
        # Inisialisasi log
        self.log("Aplikasi siap digunakan")
    
    def log(self, message):
        """Menambahkan pesan ke area log"""
        from datetime import datetime
        timestamp = datetime.now().strftime("%H:%M:%S")
        log_message = f"[{timestamp}] {message}\n"
        self.log_text.insert(tk.END, log_message)
        self.log_text.see(tk.END)  # Auto-scroll ke akhir log
        
    def browse_json_file(self):
        filename = filedialog.askopenfilename(
            title="Pilih File JSON atau TXT",
            filetypes=[("JSON files", "*.json"), ("Text files", "*.txt"), ("All files", "*.*")]
        )
        if filename:
            self.json_file_path.set(filename)
            # Set default xlsx filename based on json filename
            json_name = os.path.splitext(os.path.basename(filename))[0]
            default_xlsx = os.path.join(os.path.dirname(filename), f"{json_name}.xlsx")
            self.xlsx_file_path.set(default_xlsx)
            self.log(f"File JSON dipilih: {filename}")
    
    def browse_xlsx_file(self):
        filename = filedialog.asksaveasfilename(
            title="Simpan Sebagai File XLSX",
            filetypes=[("Excel files", "*.xlsx"), ("All files", "*.*")],
            defaultextension=".xlsx"
        )
        if filename:
            self.xlsx_file_path.set(filename)
            self.log(f"File XLSX akan disimpan di: {filename}")
    
    def convert_file(self):
        json_path = self.json_file_path.get()
        xlsx_path = self.xlsx_file_path.get()
        
        if not json_path or not xlsx_path:
            error_msg = "Silakan pilih file JSON dan tentukan lokasi file XLSX"
            self.log(f"ERROR: {error_msg}")
            messagebox.showerror("Error", error_msg)
            return
        
        self.log(f"Memulai konversi dari {json_path} ke {xlsx_path}")
        success, message = json_to_xlsx(json_path, xlsx_path)
        
        if success:
            self.status_var.set(message)
            self.log(f"SUKSES: {message}")
            messagebox.showinfo("Sukses", message)
        else:
            self.status_var.set(message)
            self.log(f"ERROR: {message}")
            messagebox.showerror("Error", message)

# Menjalankan aplikasi
if __name__ == "__main__":
    root = tk.Tk()
    app = JsonToXlsxApp(root)
    root.mainloop()
