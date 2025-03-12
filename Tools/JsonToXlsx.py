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
        
        # Menyimpan DataFrame ke file ExcelSplitter-2.0
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
        self.root.geometry("650x450")  # Ukuran lebih besar untuk tampilan lebih baik
        self.root.resizable(True, True)  # Biarkan resizable untuk tampilan yang lebih fleksibel
        
        # Variabel untuk menyimpan path file
        self.json_file_path = tk.StringVar()
        self.xlsx_file_path = tk.StringVar()
        
        # Konfigurasi tema dan style
        self.setup_styles()
        
        # Membuat frame utama dengan padding yang lebih besar
        main_frame = ttk.Frame(root, padding="20")
        main_frame.pack(fill="both", expand=True)
        
        # Judul aplikasi dengan garis bawah visual
        header_frame = ttk.Frame(main_frame)
        header_frame.pack(fill="x", pady=(0, 15))
        
        ttk.Label(header_frame, text="Konversi File JSON ke XLSX", 
                 font=("Arial", 18, "bold"), style="Header.TLabel").pack(pady=(0, 5))
        ttk.Label(header_frame, text="by Mahendra", 
                 font=("Arial", 10), style="Subheader.TLabel").pack()
        
        # Garis pembatas
        separator = ttk.Separator(header_frame, orient="horizontal")
        separator.pack(fill="x", pady=(5, 0))
        
        # Frame untuk input dan output dalam satu container
        io_frame = ttk.LabelFrame(main_frame, text="File Input/Output", padding=(15, 10))
        io_frame.pack(fill="x", pady=10)
        
        # Input file JSON
        json_frame = ttk.Frame(io_frame)
        json_frame.pack(fill="x", pady=(5, 10))
        ttk.Label(json_frame, text="File JSON/TXT:", width=12).pack(side="left", padx=5)
        ttk.Entry(json_frame, textvariable=self.json_file_path, width=50).pack(side="left", padx=5, fill="x", expand=True)
        ttk.Button(json_frame, text="Pilih File", command=self.browse_json_file, style="Action.TButton").pack(side="left", padx=(5, 0))
        
        # Output file XLSX
        xlsx_frame = ttk.Frame(io_frame)
        xlsx_frame.pack(fill="x", pady=(0, 5))
        ttk.Label(xlsx_frame, text="File XLSX:", width=12).pack(side="left", padx=5)
        ttk.Entry(xlsx_frame, textvariable=self.xlsx_file_path, width=50).pack(side="left", padx=5, fill="x", expand=True)
        ttk.Button(xlsx_frame, text="Pilih File", command=self.browse_xlsx_file, style="Action.TButton").pack(side="left", padx=(5, 0))
        
        # Tombol konversi dengan frame tersendiri
        button_frame = ttk.Frame(main_frame)
        button_frame.pack(fill="x", pady=10)
        convert_button = ttk.Button(button_frame, text="Konversi", command=self.convert_file, style="Accent.TButton")
        convert_button.pack(pady=5, padx=100)
        
        # Area log dengan tampilan yang ditingkatkan
        log_frame = ttk.LabelFrame(main_frame, text="Log Aktivitas", padding=(10, 5))
        log_frame.pack(fill="both", expand=True, pady=10)
        
        # Frame untuk text log dan scrollbar
        log_container = ttk.Frame(log_frame)
        log_container.pack(fill="both", expand=True)
        
        # Scrollbar untuk area log
        scrollbar = ttk.Scrollbar(log_container)
        scrollbar.pack(side="right", fill="y")
        
        # Text widget dengan warna latar dan font yang lebih baik
        self.log_text = tk.Text(log_container, height=8, wrap="word", 
                              font=("Consolas", 9), bg="#f9f9f9", fg="#333333")
        self.log_text.pack(side="left", fill="both", expand=True)
        
        # Konfigurasi scrollbar
        self.log_text.config(yscrollcommand=scrollbar.set)
        scrollbar.config(command=self.log_text.yview)
        
        # Status bar yang lebih jelas
        self.status_var = tk.StringVar()
        self.status_var.set("Siap untuk konversi")
        status_bar = ttk.Label(root, textvariable=self.status_var, relief="sunken", 
                              anchor="w", padding=(10, 2), style="Status.TLabel")
        status_bar.pack(fill="x", side="bottom")
        
        # Inisialisasi log
        self.log("Aplikasi siap digunakan. Silakan pilih file JSON untuk dikonversi.")
    
    def setup_styles(self):
        """Konfigurasi style untuk tampilan yang lebih modern"""
        style = ttk.Style()
        
        # Warna tema
        primary_color = "#4a86e8"
        secondary_color = "#63b1f2"
        bg_color = "#f0f0f0"
        
        # Konfigurasi tombol utama
        style.configure("Accent.TButton", 
                       font=("Arial", 11, "bold"),
                       padding=(20, 10),
                       background=primary_color)
        
        # Tombol aksi (pilih file)
        style.configure("Action.TButton",
                       padding=(10, 5))
        
        # Label header
        style.configure("Header.TLabel", 
                       foreground=primary_color)
        
        # Label subheader
        style.configure("Subheader.TLabel", 
                       foreground="#666666")
        
        # Status bar
        style.configure("Status.TLabel", 
                       background="#f0f0f0", 
                       font=("Arial", 9))
                       
        # Konfigurasi frame
        style.configure("TLabelframe", 
                       borderwidth=1, 
                       relief="solid")
        
        # Label frame header
        style.configure("TLabelframe.Label", 
                       font=("Arial", 10, "bold"),
                       foreground=primary_color)
    
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
