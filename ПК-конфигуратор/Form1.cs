using System;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ПК_конфигуратор
{
    public partial class Form1 : Form
    {
        public static string database = Directory.GetCurrentDirectory() + "\\CPU.mdb";
        public static string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + database + ";Persist Security Info=True";
        private OleDbConnection myConnection;
        string current_cpu = ""; 
        string current_comp = "";
        string current_motherboard = "";
        string current_case = "";
        string current_videocard = "";
        string current_ps = "";
        string current_ram = "";
        string current_storage = "";
        string current_cooler = "";
        public Form1()
        {
            InitializeComponent();
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel6.Controls.Clear();
            panel7.Controls.Clear();
            current_comp = "cpu";

            // текст запроса
            string query = "SELECT * FROM CPU";

            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand(query, myConnection);

            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            OleDbDataReader reader = command.ExecuteReader();

            Label name_cpu = new Label();
            name_cpu.Text = "Название";
            name_cpu.Location = new System.Drawing.Point(181, 15);
            panel6.Controls.Add(name_cpu);

            Label name_cpu_core = new Label();
            name_cpu_core.Text = "Количество ядер";
            name_cpu_core.Location = new System.Drawing.Point(350, 15);
            panel6.Controls.Add(name_cpu_core);

            Label name_ghz = new Label();
            name_ghz.Text = "Частота";
            name_ghz.Location = new System.Drawing.Point(500, 15);
            panel6.Controls.Add(name_ghz);

            Label name_tdp = new Label();
            name_tdp.Text = "TDP";
            name_tdp.Location = new System.Drawing.Point(650, 15);
            panel6.Controls.Add(name_tdp);

            Label name_video = new Label();
            name_video.Text = "Встроенная графика";
            name_video.Location = new System.Drawing.Point(820, 15);
            panel6.Controls.Add(name_video);

            Label name_price = new Label();
            name_price.Text = "Цена";
            name_price.Location = new System.Drawing.Point(1050, 15);
            panel6.Controls.Add(name_price);

            name_cpu.AutoSize = name_cpu_core.AutoSize = name_ghz.AutoSize = name_tdp.AutoSize = name_video.AutoSize = name_price.AutoSize = true;
            name_cpu.Font = name_cpu_core.Font = name_ghz.Font = name_tdp.Font = name_video.Font = name_price.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

            int y = 0;
            while (reader.Read())
            {
                //Название
                string cpu_txt = reader[1].ToString();
                string cpu_core_txt = reader[2].ToString();
                string ghz_txt = reader[3].ToString();
                string tdp_txt = reader[4].ToString();
                string video_txt = reader[5].ToString();
                string price_txt = reader[6].ToString();
                string picture = reader[7].ToString();

                //Создание панели
                Panel lb = new Panel();
                lb.Name = cpu_txt;

                if (current_cpu == cpu_txt)
                {
                    lb.BackColor = Color.FromArgb(158, 158, 158);
                }

                lb.Click += Lb_Click;

                //Указываем расположение
                lb.Location = new System.Drawing.Point(0, 0 + y);

                //Задаем размер
                lb.Size = new Size(1136, 77);

                //Добавляем на панель
                panel7.Controls.Add(lb);

                //Картинка
                Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + "\\image\\" + picture + ".jpg");
                PictureBox pic = new PictureBox();
                pic.Size = new Size(175, 77);
                pic.Location = new Point(0, 0);
                pic.Image = image;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                lb.Controls.Add(pic);

                //Создаем label
                Label cpu = new Label();
                Label cpu_core = new Label();
                Label ghz = new Label();
                Label tdp = new Label();
                Label video = new Label();
                Label price = new Label();

                //Добавляем текст
                cpu.Text = cpu_txt;
                cpu_core.Text = cpu_core_txt;
                ghz.Text = ghz_txt;
                tdp.Text = tdp_txt;
                video.Text = video_txt;
                price.Text = price_txt;

                cpu.AutoSize = cpu_core.AutoSize = ghz.AutoSize = tdp.AutoSize = video.AutoSize = price.AutoSize = true;
                cpu.Font = cpu_core.Font = ghz.Font = tdp.Font = video.Font = price.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                //координаты
                cpu.Location = new System.Drawing.Point(181, 30);
                cpu_core.Location = new System.Drawing.Point(350, 30);
                ghz.Location = new System.Drawing.Point(500, 30);
                tdp.Location = new System.Drawing.Point(650, 30);
                video.Location = new System.Drawing.Point(820, 30);
                price.Location = new System.Drawing.Point(1050, 30);

                //Добавляем на созданную ранее панель
                lb.Controls.Add(cpu);
                lb.Controls.Add(cpu_core);
                lb.Controls.Add(ghz);
                lb.Controls.Add(tdp);
                lb.Controls.Add(video);
                lb.Controls.Add(price);

                //Координаты для панелей
                y += 78;

            }
        }

        private void Lb_Click(object sender, EventArgs e)
        {

            if (sender is Panel pan)
            {
                if (pan.BackColor == Color.FromArgb(158, 158, 158))
                {
                    pan.BackColor = Color.FromArgb(255, 255, 255);
                    if (current_comp == "cpu")
                    {
                        current_cpu = "";
                    }

                    else if (current_comp == "motherboard")
                    {
                        current_motherboard = "";
                    }
                    else if (current_comp == "case")
                    {
                        current_case = "";
                    }
                    else if (current_comp == "videocard")
                    {
                        current_videocard = "";
                    }
                    else if (current_comp == "ps")
                    {
                        current_ps = "";
                    }
                    else if (current_comp == "ram")
                    {
                        current_ram = "";
                    }
                    else if (current_comp == "storage")
                    {
                        current_storage = "";
                    }
                    else if (current_comp == "cooler")
                    {
                        current_cooler = "";
                    }
                }
                else
                {
                    for (int i = 0; i < panel7.Controls.Count; i++)
                    {
                        panel7.Controls[i].BackColor = Color.FromArgb(255, 255, 255);
                    }
                    pan.BackColor = Color.FromArgb(158, 158, 158);
                    if (current_comp == "cpu")
                    {
                        current_cpu = pan.Name.ToString();
                    }

                    if (current_comp == "motherboard")
                    {
                        current_motherboard = pan.Name.ToString();
                    }
                    if (current_comp == "case")
                    {
                        current_case = pan.Name.ToString();
                    }
                    if (current_comp == "videocard")
                    {
                        current_videocard = pan.Name.ToString();
                    }
                    if (current_comp == "ps")
                    {
                        current_ps = pan.Name.ToString();
                    }
                    if (current_comp == "ram")
                    {
                        current_ram = pan.Name.ToString();
                    }
                    if (current_comp == "storage")
                    {
                        current_storage = pan.Name.ToString();
                    }
                    if (current_comp == "cooler")
                    {
                        current_cooler = pan.Name.ToString();
                    }

                }

            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            panel6.Controls.Clear();
            panel7.Controls.Clear();
            current_comp = "motherboard";

            // текст запроса
            string query = "SELECT * FROM Motherboard";

            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand(query, myConnection);

            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            OleDbDataReader reader = command.ExecuteReader();

            Label name_mb = new Label();
            name_mb.Text = "Название";
            name_mb.Location = new System.Drawing.Point(181, 15);
            panel6.Controls.Add(name_mb);

            Label name_mb_socket = new Label();
            name_mb_socket.Text = "Сокет";
            name_mb_socket.Location = new System.Drawing.Point(400, 15);
            panel6.Controls.Add(name_mb_socket);

            Label name_mb_form = new Label();
            name_mb_form.Text = "Форм фактор";
            name_mb_form.Location = new System.Drawing.Point(500, 15);
            panel6.Controls.Add(name_mb_form);

            Label name_mb_maxram = new Label();
            name_mb_maxram.Text = "Макс. объем памяти";
            name_mb_maxram.Location = new System.Drawing.Point(650, 15);
            panel6.Controls.Add(name_mb_maxram);

            Label name_mb_ramslots = new Label();
            name_mb_ramslots.Text = "Кол-во слотов памяти ";
            name_mb_ramslots.Location = new System.Drawing.Point(820, 15);
            panel6.Controls.Add(name_mb_ramslots);

            Label name_mb_price = new Label();
            name_mb_price.Text = "Цена";
            name_mb_price.Location = new System.Drawing.Point(1050, 15);
            panel6.Controls.Add(name_mb_price);

            name_mb.AutoSize = name_mb_socket.AutoSize = name_mb_form.AutoSize = name_mb_maxram.AutoSize = name_mb_ramslots.AutoSize = name_mb_price.AutoSize = true;
            name_mb.Font = name_mb_socket.Font = name_mb_form.Font = name_mb_maxram.Font = name_mb_ramslots.Font = name_mb_price.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

            int y = 0;
            while (reader.Read())
            {
                //Название
                string mb_txt = reader[1].ToString();
                string mb_socket_txt = reader[2].ToString();
                string mb_form_txt = reader[3].ToString();
                string mb_maxram_txt = reader[4].ToString();
                string mb_ramslots_txt = reader[5].ToString();
                string mb_price_txt = reader[6].ToString();
                string mb_picture = reader[7].ToString();

                //Создаем панель
                Panel lb = new Panel();
                lb.Name = mb_txt;

                if (current_motherboard == mb_txt)
                {
                    lb.BackColor = Color.FromArgb(158, 158, 158);
                }

                lb.Click += Lb_Click;

                //Указываем расположение
                lb.Location = new System.Drawing.Point(0, 0 + y);

                //Задаем размер
                lb.Size = new Size(1136, 77);


                //Добавляем на панель
                panel7.Controls.Add(lb);

                //Картинка
                Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + "\\image\\" + mb_picture + ".jpg");
                PictureBox pic = new PictureBox();
                pic.Size = new Size(175, 77);
                pic.Location = new Point(0, 0);
                pic.Image = image;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                lb.Controls.Add(pic);

                //Создаем label
                Label mb = new Label();
                Label mb_socket = new Label();
                Label mb_form = new Label();
                Label mb_maxram = new Label();
                Label mb_ramslots = new Label();
                Label mb_price = new Label();

                //Добавляем текст
                mb.Text = mb_txt;
                mb_socket.Text = mb_socket_txt;
                mb_form.Text = mb_form_txt;
                mb_maxram.Text = mb_maxram_txt;
                mb_ramslots.Text = mb_ramslots_txt;
                mb_price.Text = mb_price_txt;

                mb.AutoSize = mb_socket.AutoSize = mb_form.AutoSize = mb_maxram.AutoSize = mb_ramslots.AutoSize = mb_price.AutoSize = true;
                mb.Font = mb_socket.Font = mb_form.Font = mb_maxram.Font = mb_ramslots.Font = mb_price.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                //координаты
                mb.Location = new System.Drawing.Point(181, 30);
                mb_socket.Location = new System.Drawing.Point(400, 30);
                mb_form.Location = new System.Drawing.Point(500, 30);
                mb_maxram.Location = new System.Drawing.Point(650, 30);
                mb_ramslots.Location = new System.Drawing.Point(820, 30);
                mb_price.Location = new System.Drawing.Point(1050, 30);

                //Добавляем на созданную ранее панель
                lb.Controls.Add(mb);
                lb.Controls.Add(mb_socket);
                lb.Controls.Add(mb_form);
                lb.Controls.Add(mb_maxram);
                lb.Controls.Add(mb_ramslots);
                lb.Controls.Add(mb_price);

                //Координаты для панелей
                y += 78;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel6.Controls.Clear();
            panel7.Controls.Clear();
            current_comp = "case";

            // текст запроса
            string query = "SELECT * FROM Cases";

            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand(query, myConnection);

            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            OleDbDataReader reader = command.ExecuteReader();

            Label name_case = new Label();
            name_case.Text = "Название";
            name_case.Location = new System.Drawing.Point(181, 15);
            panel6.Controls.Add(name_case);

            Label name_case_type = new Label();
            name_case_type.Text = "Тип";
            name_case_type.Location = new System.Drawing.Point(400, 15);
            panel6.Controls.Add(name_case_type);

            Label name_case_color = new Label();
            name_case_color.Text = "Цвет";
            name_case_color.Location = new System.Drawing.Point(600, 15);
            panel6.Controls.Add(name_case_color);

            Label name_PS = new Label();//PS=power supply = блок питания
            name_PS.Text = "Блок питания";
            name_PS.Location = new System.Drawing.Point(800, 15);
            panel6.Controls.Add(name_PS);

            Label name_case_price = new Label();
            name_case_price.Text = "Цена";
            name_case_price.Location = new System.Drawing.Point(1050, 15);
            panel6.Controls.Add(name_case_price);

            name_case.AutoSize = name_case_type.AutoSize = name_case_color.AutoSize = name_PS.AutoSize = name_case_price.AutoSize = true;
            name_case.Font = name_case_type.Font = name_case_color.Font = name_PS.Font = name_case_price.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

            int y = 0;
            while (reader.Read())
            {
                //Название
                string case_txt = reader[1].ToString();
                string case_type_txt = reader[2].ToString();
                string case_color_txt = reader[3].ToString();
                string case_PS_txt = reader[4].ToString();
                string case_price_txt = reader[5].ToString();
                string case_picture = reader[6].ToString();

                //Создаем панель
                Panel lb = new Panel();
                lb.Name = case_txt;

                if (current_case == case_txt)
                {
                    lb.BackColor = Color.FromArgb(158, 158, 158);
                }

                //Указываем расположение
                lb.Location = new System.Drawing.Point(0, 0 + y);

                //Задаем размер
                lb.Size = new Size(1136, 77);


                //Добавляем на панель
                panel7.Controls.Add(lb);

                lb.Click += Lb_Click;

                //Картинка
                Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + "\\image\\" + case_picture + ".jpg");
                PictureBox pic = new PictureBox();
                pic.Size = new Size(175, 77);
                pic.Location = new Point(0, 0);
                pic.Image = image;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                lb.Controls.Add(pic);

                //Создаем label
                Label case_name = new Label();
                Label case_type = new Label();
                Label case_color = new Label();
                Label case_PS = new Label();
                Label case_price = new Label();

                //Добавляем текст
                case_name.Text = case_txt;
                case_type.Text = case_type_txt;
                case_color.Text = case_color_txt;
                case_PS.Text = case_PS_txt;
                case_price.Text = case_price_txt;

                case_name.AutoSize = case_type.AutoSize = case_color.AutoSize = case_PS.AutoSize = case_price.AutoSize = true;
                case_name.Font = case_type.Font = case_color.Font = case_PS.Font = case_price.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                //координаты
                case_name.Location = new System.Drawing.Point(181, 30);
                case_type.Location = new System.Drawing.Point(400, 30);
                case_color.Location = new System.Drawing.Point(600, 30);
                case_PS.Location = new System.Drawing.Point(800, 30);
                case_price.Location = new System.Drawing.Point(1050, 30);

                //Добавляем на созданную ранее панель
                lb.Controls.Add(case_name);
                lb.Controls.Add(case_type);
                lb.Controls.Add(case_color);
                lb.Controls.Add(case_PS);
                lb.Controls.Add(case_price);

                //Координаты для панелей
                y += 78;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel6.Controls.Clear();
            panel7.Controls.Clear();
            current_comp = "videocard";

            // текст запроса
            string query = "SELECT * FROM Videocard";

            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand(query, myConnection);

            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            OleDbDataReader reader = command.ExecuteReader();

            Label name_video = new Label();
            name_video.Text = "Название";
            name_video.Location = new System.Drawing.Point(181, 15);
            panel6.Controls.Add(name_video);

            Label name_video_chipset = new Label();
            name_video_chipset.Text = "Чипсет";
            name_video_chipset.Location = new System.Drawing.Point(400, 15);
            panel6.Controls.Add(name_video_chipset);

            Label name_video_vram = new Label();
            name_video_vram.Text = "Видеопамять";
            name_video_vram.Location = new System.Drawing.Point(600, 15);
            panel6.Controls.Add(name_video_vram);

            Label name_video_coreclock = new Label();//PS=power supply = блок питания
            name_video_coreclock.Text = "Частота ядра ";
            name_video_coreclock.Location = new System.Drawing.Point(750, 15);
            panel6.Controls.Add(name_video_coreclock);

            Label name_video_color = new Label();
            name_video_color.Text = "Цвет";
            name_video_color.Location = new System.Drawing.Point(920, 15);
            panel6.Controls.Add(name_video_color);

            Label name_video_price = new Label();
            name_video_price.Text = "Цена";
            name_video_price.Location = new System.Drawing.Point(1050, 15);
            panel6.Controls.Add(name_video_price);

            name_video.AutoSize = name_video_chipset.AutoSize = name_video_vram.AutoSize = name_video_coreclock.AutoSize = name_video_color.AutoSize = name_video_price.AutoSize = true;
            name_video.Font = name_video_chipset.Font = name_video_vram.Font = name_video_coreclock.Font = name_video_color.Font = name_video_price.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

            int y = 0;

            while (reader.Read())
            {
                //Название
                string video_txt = reader[1].ToString();
                string video_chipset_txt = reader[2].ToString();
                string video_vram_txt = reader[3].ToString();
                string video_coreclock_txt = reader[4].ToString();
                string video_color_txt = reader[5].ToString();
                string video_price_txt = reader[6].ToString();
                string video_picture = reader[7].ToString();

                //Создаем панель
                Panel lb = new Panel();
                lb.Name = video_txt;

                if (current_videocard == video_txt)
                {
                    lb.BackColor = Color.FromArgb(158, 158, 158);
                }

                lb.Click += Lb_Click;

                //Указываем расположение
                lb.Location = new System.Drawing.Point(0, 0 + y);

                //Задаем размер
                lb.Size = new Size(1136, 77);

                //Добавляем на панель
                panel7.Controls.Add(lb);

                //Картинка
                Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + "\\image\\" + video_picture + ".jpg");
                PictureBox pic = new PictureBox();
                pic.Size = new Size(175, 77);
                pic.Location = new Point(0, 0);
                pic.Image = image;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                lb.Controls.Add(pic);

                //Создаем label
                Label video_name = new Label();
                Label video_chipset = new Label();
                Label video_vram = new Label();
                Label video_coreclock = new Label();
                Label video_color = new Label();
                Label video_price = new Label();


                //Добавляем текст
                video_name.Text = video_txt;
                video_chipset.Text = video_chipset_txt;
                video_vram.Text = video_vram_txt;
                video_coreclock.Text = video_coreclock_txt;
                video_color.Text = video_color_txt;
                video_price.Text = video_price_txt;

                video_name.AutoSize = video_chipset.AutoSize = video_vram.AutoSize = video_coreclock.AutoSize = video_color.AutoSize = video_price.AutoSize = true;
                video_name.Font = video_chipset.Font = video_vram.Font = video_coreclock.Font = video_color.Font = video_color.Font = video_price.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

                //координаты
                video_name.Location = new System.Drawing.Point(181, 30);
                video_chipset.Location = new System.Drawing.Point(400, 30);
                video_vram.Location = new System.Drawing.Point(600, 30);
                video_coreclock.Location = new System.Drawing.Point(750, 30);
                video_color.Location = new System.Drawing.Point(920, 30);
                video_price.Location = new System.Drawing.Point(1050, 30);

                //Добавляем на созданную ранее панель
                lb.Controls.Add(video_name);
                lb.Controls.Add(video_chipset);
                lb.Controls.Add(video_vram);
                lb.Controls.Add(video_coreclock);
                lb.Controls.Add(video_color);
                lb.Controls.Add(video_price);

                //Координаты для панелей
                y += 78;

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel6.Controls.Clear();
            panel7.Controls.Clear();
            current_comp = "ram";

            // текст запроса
            string query = "SELECT * FROM Ram";

            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand(query, myConnection);

            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            OleDbDataReader reader = command.ExecuteReader();

            Label name_ram = new Label();
            name_ram.Text = "Название";
            name_ram.Location = new System.Drawing.Point(181, 15);
            panel6.Controls.Add(name_ram);

            Label name_ram_mhz = new Label();
            name_ram_mhz.Text = "Частота";
            name_ram_mhz.Location = new System.Drawing.Point(430, 15);
            panel6.Controls.Add(name_ram_mhz);

            Label name_ram_type = new Label();
            name_ram_type.Text = "Тип";
            name_ram_type.Location = new System.Drawing.Point(530, 15);
            panel6.Controls.Add(name_ram_type);

            Label name_ram_color = new Label();
            name_ram_color.Text = "Цвет ";
            name_ram_color.Location = new System.Drawing.Point(650, 15);
            panel6.Controls.Add(name_ram_color);

            Label name_ram_module = new Label();
            name_ram_module.Text = "Кол-во модулей";
            name_ram_module.Location = new System.Drawing.Point(850, 15);
            panel6.Controls.Add(name_ram_module);

            Label name_ram_price = new Label();
            name_ram_price.Text = "Цена";
            name_ram_price.Location = new System.Drawing.Point(1050, 15);
            panel6.Controls.Add(name_ram_price);

            name_ram.AutoSize = name_ram_mhz.AutoSize = name_ram_type.AutoSize = name_ram_color.AutoSize = name_ram_module.AutoSize = name_ram_price.AutoSize = true;
            name_ram.Font = name_ram_mhz.Font = name_ram_type.Font = name_ram_color.Font = name_ram_module.Font = name_ram_price.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

            int y = 0;

            while (reader.Read())
            {
                //Название
                string ram_txt = reader[1].ToString();
                string ram_mhz_txt = reader[2].ToString();
                string ram_type_txt = reader[3].ToString();
                string ram_color_txt = reader[4].ToString();
                string ram_module_txt = reader[5].ToString();
                string ram_price_txt = reader[6].ToString();
                string ram_picture = reader[7].ToString();

                //Создаем панель
                Panel lb = new Panel();
                lb.Name = ram_txt;

                if (current_ram == ram_txt)
                {
                    lb.BackColor = Color.FromArgb(158, 158, 158);
                }

                lb.Click += Lb_Click;

                //Указываем расположение
                lb.Location = new System.Drawing.Point(0, 0 + y);

                //Задаем размер
                lb.Size = new Size(1136, 77);

                //Добавляем на панель
                panel7.Controls.Add(lb);

                //Картинка
                Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + "\\image\\" + ram_picture + ".jpg");
                PictureBox pic = new PictureBox();
                pic.Size = new Size(175, 77);
                pic.Location = new Point(0, 0);
                pic.Image = image;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                lb.Controls.Add(pic);

                //Создаем label
                Label ram_name = new Label();
                Label ram_mhz = new Label();
                Label ram_type = new Label();
                Label ram_color = new Label();
                Label ram_module = new Label();
                Label ram_price = new Label();


                //Добавляем текст
                ram_name.Text = ram_txt;
                ram_mhz.Text = ram_mhz_txt;
                ram_type.Text = ram_type_txt;
                ram_color.Text = ram_color_txt;
                ram_module.Text = ram_module_txt;
                ram_price.Text = ram_price_txt;

                ram_name.AutoSize = ram_mhz.AutoSize = ram_type.AutoSize = ram_color.AutoSize = ram_color.AutoSize = ram_module.AutoSize = ram_price.AutoSize = true;
                ram_name.Font = ram_mhz.Font = ram_type.Font = ram_color.Font = ram_color.Font = ram_module.Font = ram_price.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

                //координаты
                ram_name.Location = new System.Drawing.Point(181, 30);
                ram_mhz.Location = new System.Drawing.Point(430, 30);
                ram_type.Location = new System.Drawing.Point(530, 30);
                ram_color.Location = new System.Drawing.Point(650, 30);
                ram_module.Location = new System.Drawing.Point(850, 30);
                ram_price.Location = new System.Drawing.Point(1050, 30);

                //Добавляем на созданную ранее панель
                lb.Controls.Add(ram_name);
                lb.Controls.Add(ram_mhz);
                lb.Controls.Add(ram_type);
                lb.Controls.Add(ram_color);
                lb.Controls.Add(ram_module);
                lb.Controls.Add(ram_price);

                //Координаты для панелей
                y += 78;

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel6.Controls.Clear();
            panel7.Controls.Clear();
            current_comp = "cooler";

            // текст запроса
            string query = "SELECT * FROM Cooler";

            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand(query, myConnection);

            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            OleDbDataReader reader = command.ExecuteReader();

            Label name_cooler = new Label();
            name_cooler.Text = "Название";
            name_cooler.Location = new System.Drawing.Point(181, 15);
            panel6.Controls.Add(name_cooler);

            Label name_cooler_rpm = new Label();
            name_cooler_rpm.Text = "Кол-во оборотов";
            name_cooler_rpm.Location = new System.Drawing.Point(450, 15);
            panel6.Controls.Add(name_cooler_rpm);

            Label name_cooler_noise = new Label();
            name_cooler_noise.Text = "Уровень шума";
            name_cooler_noise.Location = new System.Drawing.Point(650, 15);
            panel6.Controls.Add(name_cooler_noise);

            Label name_cooler_color = new Label();
            name_cooler_color.Text = "Цвет ";
            name_cooler_color.Location = new System.Drawing.Point(850, 15);
            panel6.Controls.Add(name_cooler_color);

            Label name_cooler_price = new Label();
            name_cooler_price.Text = "Цена";
            name_cooler_price.Location = new System.Drawing.Point(1050, 15);
            panel6.Controls.Add(name_cooler_price);

            name_cooler.AutoSize = name_cooler_rpm.AutoSize = name_cooler_noise.AutoSize = name_cooler_color.AutoSize = name_cooler_price.AutoSize = true;
            name_cooler.Font = name_cooler_rpm.Font = name_cooler_noise.Font = name_cooler_color.Font = name_cooler_price.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

            int y = 0;

            while (reader.Read())
            {
                //Название
                string cooler_txt = reader[1].ToString();
                string cooler_rpm_txt = reader[2].ToString();
                string cooler_noise_txt = reader[3].ToString();
                string cooler_color_txt = reader[4].ToString();
                string cooler_price_txt = reader[5].ToString();
                string cooler_picture = reader[6].ToString();

                //Создаем панель
                Panel lb = new Panel();
                lb.Name = cooler_txt;

                if (current_cooler == cooler_txt)
                {
                    lb.BackColor = Color.FromArgb(158, 158, 158);
                }

                lb.Click += Lb_Click;

                //Указываем расположение
                lb.Location = new System.Drawing.Point(0, 0 + y);

                //Задаем размер
                lb.Size = new Size(1136, 77);

                //Добавляем на панель
                panel7.Controls.Add(lb);

                //Картинка
                Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + "\\image\\" + cooler_picture + ".jpg");
                PictureBox pic = new PictureBox();
                pic.Size = new Size(175, 77);
                pic.Location = new Point(0, 0);
                pic.Image = image;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                lb.Controls.Add(pic);

                //Создаем label
                Label cooler_name = new Label();
                Label cooler_rpm = new Label();
                Label cooler_noise = new Label();
                Label cooler_color = new Label();
                Label cooler_price = new Label();

                //Добавляем текст
                cooler_name.Text = cooler_txt;
                cooler_rpm.Text = cooler_rpm_txt;
                cooler_noise.Text = cooler_noise_txt;
                cooler_color.Text = cooler_color_txt;
                cooler_price.Text = cooler_price_txt;

                cooler_name.AutoSize = cooler_rpm.AutoSize = cooler_noise.AutoSize = cooler_color.AutoSize = cooler_price.AutoSize = true;
                cooler_name.Font = cooler_rpm.Font = cooler_noise.Font = cooler_color.Font = cooler_price.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

                //координаты
                cooler_name.Location = new System.Drawing.Point(181, 30);
                cooler_rpm.Location = new System.Drawing.Point(450, 30);
                cooler_noise.Location = new System.Drawing.Point(650, 30);
                cooler_color.Location = new System.Drawing.Point(850, 30);
                cooler_price.Location = new System.Drawing.Point(1050, 30);

                //Добавляем на созданную ранее панель
                lb.Controls.Add(cooler_name);
                lb.Controls.Add(cooler_rpm);
                lb.Controls.Add(cooler_noise);
                lb.Controls.Add(cooler_color);
                lb.Controls.Add(cooler_price);

                //Координаты для панелей
                y += 78;

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel6.Controls.Clear();
            panel7.Controls.Clear();
            current_comp = "storage";

            // текст запроса
            string query = "SELECT * FROM Storage";

            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand(query, myConnection);

            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            OleDbDataReader reader = command.ExecuteReader();

            Label name_storage = new Label();
            name_storage.Text = "Название";
            name_storage.Location = new System.Drawing.Point(181, 15);
            panel6.Controls.Add(name_storage);

            Label name_storage_memory_type = new Label();
            name_storage_memory_type.Text = "Тип памяти";
            name_storage_memory_type.Location = new System.Drawing.Point(450, 15);
            panel6.Controls.Add(name_storage_memory_type);

            Label name_storage_memory_size = new Label();
            name_storage_memory_size.Text = "Размер памяти";
            name_storage_memory_size.Location = new System.Drawing.Point(650, 15);
            panel6.Controls.Add(name_storage_memory_size);

            Label name_storage_interface = new Label();
            name_storage_interface.Text = "Интерфейс";
            name_storage_interface.Location = new System.Drawing.Point(850, 15);
            panel6.Controls.Add(name_storage_interface);

            Label name_storage_price = new Label();
            name_storage_price.Text = "Цена";
            name_storage_price.Location = new System.Drawing.Point(1050, 15);
            panel6.Controls.Add(name_storage_price);

            name_storage.AutoSize = name_storage_memory_type.AutoSize = name_storage_memory_size.AutoSize = name_storage_interface.AutoSize = name_storage_price.AutoSize = true;
            name_storage.Font = name_storage_memory_type.Font = name_storage_memory_size.Font = name_storage_interface.Font = name_storage_price.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

            int y = 0;

            while (reader.Read())
            {
                //Название
                string storage_txt = reader[1].ToString();
                string storage_memory_type_txt = reader[2].ToString();
                string storage_memory_size_txt = reader[3].ToString();
                string storage_interface_txt = reader[4].ToString();
                string storage_price_txt = reader[5].ToString();
                string storage_picture = reader[6].ToString();

                //Создаем панель
                Panel lb = new Panel();
                lb.Name = storage_txt;

                if (current_storage == storage_txt)
                {
                    lb.BackColor = Color.FromArgb(158, 158, 158);
                }

                lb.Click += Lb_Click;

                //Указываем расположение
                lb.Location = new System.Drawing.Point(0, 0 + y);

                //Задаем размер
                lb.Size = new Size(1136, 77);

                //Добавляем на панель
                panel7.Controls.Add(lb);

                //Картинка
                Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + "\\image\\" + storage_picture + ".jpg");
                PictureBox pic = new PictureBox();
                pic.Size = new Size(175, 77);
                pic.Location = new Point(0, 0);
                pic.Image = image;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                lb.Controls.Add(pic);

                //Создаем label
                Label storage_name = new Label();
                Label storage_memory_type = new Label();
                Label storage_memory_size = new Label();
                Label storage_interface = new Label();
                Label storage_price = new Label();

                //Добавляем текст
                storage_name.Text = storage_txt;
                storage_memory_type.Text = storage_memory_type_txt;
                storage_memory_size.Text = storage_memory_size_txt;
                storage_interface.Text = storage_interface_txt;
                storage_price.Text = storage_price_txt;

                storage_name.AutoSize = storage_memory_type.AutoSize = storage_memory_size.AutoSize = storage_interface.AutoSize = storage_price.AutoSize = true;
                storage_name.Font = storage_memory_type.Font = storage_memory_size.Font = storage_interface.Font = storage_price.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

                //координаты
                storage_name.Location = new System.Drawing.Point(181, 30);
                storage_memory_type.Location = new System.Drawing.Point(450, 30);
                storage_memory_size.Location = new System.Drawing.Point(650, 30);
                storage_interface.Location = new System.Drawing.Point(850, 30);
                storage_price.Location = new System.Drawing.Point(1050, 30);

                //Добавляем на созданную ранее панель
                lb.Controls.Add(storage_name);
                lb.Controls.Add(storage_memory_type);
                lb.Controls.Add(storage_memory_size);
                lb.Controls.Add(storage_interface);
                lb.Controls.Add(storage_price);

                //Координаты для панелей
                y += 78;

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel6.Controls.Clear();
            panel7.Controls.Clear();
            current_comp = "ps";

            // текст запроса
            string query = "SELECT * FROM Powersupply";

            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand(query, myConnection);

            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            OleDbDataReader reader = command.ExecuteReader();

            Label name_powersupply = new Label();
            name_powersupply.Text = "Название";
            name_powersupply.Location = new System.Drawing.Point(181, 15);
            panel6.Controls.Add(name_powersupply);

            Label name_powersupply_form = new Label();
            name_powersupply_form.Text = "Тип блока питания";
            name_powersupply_form.Location = new System.Drawing.Point(330, 15);
            panel6.Controls.Add(name_powersupply_form);

            Label name_powersupply_cert = new Label();
            name_powersupply_cert.Text = "Сертификация";
            name_powersupply_cert.Location = new System.Drawing.Point(500, 15);
            panel6.Controls.Add(name_powersupply_cert);

            Label name_powersupply_power = new Label();
            name_powersupply_power.Text = "Мощность";
            name_powersupply_power.Location = new System.Drawing.Point(670, 15);
            panel6.Controls.Add(name_powersupply_power);

            Label name_powersupply_module = new Label();
            name_powersupply_module.Text = "Модульность";
            name_powersupply_module.Location = new System.Drawing.Point(850, 15);
            panel6.Controls.Add(name_powersupply_module);

            Label name_powersupply_price = new Label();
            name_powersupply_price.Text = "Цена";
            name_powersupply_price.Location = new System.Drawing.Point(1050, 15);
            panel6.Controls.Add(name_powersupply_price);

            name_powersupply.AutoSize = name_powersupply_form.AutoSize = name_powersupply_cert.AutoSize = name_powersupply_power.AutoSize = name_powersupply_module.AutoSize = name_powersupply_price.AutoSize = true;
            name_powersupply.Font = name_powersupply_form.Font = name_powersupply_cert.Font = name_powersupply_power.Font = name_powersupply_module.Font = name_powersupply_price.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

            int y = 0;

            while (reader.Read())
            {
                //Название
                string powersupply_txt = reader[1].ToString();
                string powersupply_form_txt = reader[2].ToString();
                string powersupply_cert_txt = reader[3].ToString();
                string name_powersupply_power_txt = reader[4].ToString();
                string name_powersupply_module_txt = reader[5].ToString();
                string name_powersupply_price_txt = reader[6].ToString();
                string powersupply_picture = reader[7].ToString();

                //Создаем панель
                Panel lb = new Panel();
                lb.Name = powersupply_txt;

                if (current_ps == powersupply_txt)
                {
                    lb.BackColor = Color.FromArgb(158, 158, 158);
                }

                lb.Click += Lb_Click;

                //Указываем расположение
                lb.Location = new System.Drawing.Point(0, 0 + y);

                //Задаем размер
                lb.Size = new Size(1136, 77);

                //Добавляем на панель
                panel7.Controls.Add(lb);

                //Картинка
                Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + "\\image\\" + powersupply_picture + ".jpg");
                PictureBox pic = new PictureBox();
                pic.Size = new Size(175, 77);
                pic.Location = new Point(0, 0);
                pic.Image = image;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                lb.Controls.Add(pic);

                //Создаем label
                Label powersupply_name = new Label();
                Label powersupply_form = new Label();
                Label powersupply_cert = new Label();
                Label powersupply_power = new Label();
                Label powersupply_module = new Label();
                Label powersupply_price = new Label();


                //Добавляем текст
                powersupply_name.Text = powersupply_txt;
                powersupply_form.Text = powersupply_form_txt;
                powersupply_cert.Text = powersupply_cert_txt;
                powersupply_power.Text = name_powersupply_power_txt;
                powersupply_module.Text = name_powersupply_module_txt;
                powersupply_price.Text = name_powersupply_price_txt;
                powersupply_name.AutoSize = powersupply_form.AutoSize = powersupply_cert.AutoSize = powersupply_power.AutoSize = powersupply_module.AutoSize = powersupply_price.AutoSize = true;
                powersupply_name.Font = powersupply_form.Font = powersupply_cert.Font = powersupply_power.Font = powersupply_module.Font = powersupply_price.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

                //координаты
                powersupply_name.Location = new System.Drawing.Point(181, 30);
                powersupply_form.Location = new System.Drawing.Point(330, 30);
                powersupply_cert.Location = new System.Drawing.Point(500, 30);
                powersupply_power.Location = new System.Drawing.Point(670, 30);
                powersupply_module.Location = new System.Drawing.Point(850, 30);
                powersupply_price.Location = new System.Drawing.Point(1050, 30);


                //Добавляем на созданную ранее панель
                lb.Controls.Add(powersupply_name);
                lb.Controls.Add(powersupply_form);
                lb.Controls.Add(powersupply_cert);
                lb.Controls.Add(powersupply_power);
                lb.Controls.Add(powersupply_module);
                lb.Controls.Add(powersupply_price);

                //Координаты для панелей
                y += 78;

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if ((current_cpu == "") || (current_motherboard == "") || (current_cooler == "") || (current_videocard == "") || (current_case == "") || (current_ps == "") || (current_ram == "") || (current_storage == ""))
            {
                string check_comp = "";
                if (current_cpu == "")
                    check_comp += "\nПроцессор; ";
                if (current_motherboard == "")
                    check_comp += "\nМатеринская плата; ";
                if (current_cooler == "")
                    check_comp += "\nОхлаждение процессора; ";
                if (current_videocard == "")
                    check_comp += "\nВидеокарта; ";
                if (current_case == "")
                    check_comp += "\nКорпус; ";
                if (current_ps == "")
                    check_comp += "\nБлок питания; ";
                if (current_ram == "")
                    check_comp += "\nОперативная память; ";
                if (current_storage == "")
                    check_comp += "\nВнутренние накопители; ";
                MessageBox.Show("Выберите следующие комплектующие для завершения сборки:" + check_comp);
            }
            else

            {
                MessageBox.Show("Всё выбрано");

                panel6.Controls.Clear();
                panel7.Controls.Clear();

                string query = "SELECT * FROM CPU WHERE Название='" + current_cpu + "'";
                string cpu_socket = "";
                int cpu_tdp = 0;
                int cpu_price = 0;
                string cpu_picture = "";

                OleDbCommand command = new OleDbCommand(query, myConnection);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cpu_socket = reader[8].ToString();
                    cpu_tdp = Convert.ToInt32(reader[4]);
                    cpu_price = Convert.ToInt32(reader[6]);
                    cpu_picture = reader[7].ToString();
                }
                query = "SELECT * FROM Motherboard WHERE Название='" + current_motherboard + "'";
                string motherboard_socket = "";
                string motherboard_type_ram = "";
                int motherboard_ram_min = 0;
                int motherboard_ram_max = 0;
                string motherboard_type = "";
                int motherboard_price = 0;
                string motherboard_picture = "";

                command = new OleDbCommand(query, myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    motherboard_socket = reader[2].ToString();
                    motherboard_type = reader[3].ToString();
                    motherboard_type_ram = reader[8].ToString();
                    motherboard_ram_min = Convert.ToInt32(reader[9]);
                    motherboard_ram_max = Convert.ToInt32(reader[10]);
                    motherboard_price = Convert.ToInt32(reader[6]);
                    motherboard_picture = reader[7].ToString();
                }
                query = "SELECT * FROM Videocard WHERE Название='" + current_videocard + "'";
                string videocard_lowprofile = "";
                int videocard_tdp = 0;
                int videocard_price = 0;
                string videocard_picture = "";

                command = new OleDbCommand(query, myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    videocard_lowprofile = reader[8].ToString();
                    videocard_tdp = Convert.ToInt32(reader[9]);
                    videocard_price = Convert.ToInt32(reader[6]);
                    videocard_picture = reader[7].ToString();
                }

                query = "SELECT * FROM Ram WHERE Название='" + current_ram + "'";
                string ram_type = "";
                int ram_mhz = 0;
                int ram_price = 0;
                string ram_picture = "";

                command = new OleDbCommand(query, myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ram_type = reader[3].ToString();
                    ram_mhz = Convert.ToInt32(reader[2]);
                    ram_price = Convert.ToInt32(reader[6]);
                    ram_picture = reader[7].ToString();
                }
                query = "SELECT * FROM Cases WHERE Название='" + current_case + "'";
                string case_type = "";
                int case_price = 0;
                string case_picture = "";

                command = new OleDbCommand(query, myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    case_type = reader[7].ToString();
                    case_price = Convert.ToInt32(reader[5]);
                    case_picture = reader[6].ToString();
                }
                query = "SELECT * FROM Powersupply WHERE Название='" + current_ps + "'";
                int ps_tdp = 0;
                int ps_price = 0;
                string ps_picture = "";

                command = new OleDbCommand(query, myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ps_tdp = Convert.ToInt32(reader[4]);
                    ps_price = Convert.ToInt32(reader[6]);
                    ps_picture = reader[7].ToString();
                }
                query = "SELECT * FROM Cooler WHERE Название='" + current_cooler + "'";
                string cooler_picture = "";
                int cooler_price = 0;

                command = new OleDbCommand(query, myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cooler_picture = reader[6].ToString();
                    cooler_price = Convert.ToInt32(reader[5]);
                }

                query = "SELECT * FROM Storage WHERE Название='" + current_storage + "'";
                string storage_picture = "";
                int storage_price = 0;


                command = new OleDbCommand(query, myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    storage_picture = reader[6].ToString();
                    storage_price = Convert.ToInt32(reader[5]);
                }

                Label name_item = new Label();
                name_item.Text = "Наименование";
                name_item.Location = new System.Drawing.Point(181, 15);
                panel6.Controls.Add(name_item);

                Label name_price = new Label();
                name_price.Text = "Цена";
                name_price.Location = new System.Drawing.Point(1000, 15);
                panel6.Controls.Add(name_price);

                name_item.AutoSize = name_price.AutoSize = true;
                name_item.Font = name_price.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

                //вывод проц
                PictureBox cpu_pic = new PictureBox();
                cpu_pic.Size = new Size(175, 77);
                cpu_pic.Location = new Point(0, 0); //y+76
                cpu_pic.Image = new Bitmap(Directory.GetCurrentDirectory() + "\\image\\" + cpu_picture + ".jpg");
                cpu_pic.SizeMode = PictureBoxSizeMode.Zoom;
                panel7.Controls.Add(cpu_pic);

                Label name_cpu = new Label();
                name_cpu.Text = current_cpu;
                name_cpu.Location = new System.Drawing.Point(181, 15);
                panel7.Controls.Add(name_cpu);

                Label name_cpu_price = new Label();
                name_cpu_price.Text = Convert.ToString(cpu_price);
                name_cpu_price.Location = new System.Drawing.Point(1000, 15);
                panel7.Controls.Add(name_cpu_price);

                // Вывод вывод материнки
                PictureBox mb_pic = new PictureBox();
                mb_pic.Size = new Size(175, 77);
                mb_pic.Location = new Point(0, 76); //y+76
                mb_pic.Image = new Bitmap(Directory.GetCurrentDirectory() + "\\image\\" + motherboard_picture + ".jpg");
                mb_pic.SizeMode = PictureBoxSizeMode.Zoom;
                panel7.Controls.Add(mb_pic);

                Label name_mb = new Label();
                name_mb.Text = current_motherboard;
                name_mb.Location = new System.Drawing.Point(181, 91);
                panel7.Controls.Add(name_mb);

                Label name_mb_price = new Label();
                name_mb_price.Text = Convert.ToString(motherboard_price);
                name_mb_price.Location = new System.Drawing.Point(1000, 91);
                panel7.Controls.Add(name_mb_price);

                // Вывод корпуса
                PictureBox case_pic = new PictureBox();
                case_pic.Size = new Size(175, 77);
                case_pic.Location = new Point(0, 152); //y+76
                case_pic.Image = new Bitmap(Directory.GetCurrentDirectory() + "\\image\\" + case_picture + ".jpg");
                case_pic.SizeMode = PictureBoxSizeMode.Zoom;
                panel7.Controls.Add(case_pic);

                Label name_case = new Label();
                name_case.Text = current_case;
                name_case.Location = new System.Drawing.Point(181, 167);
                panel7.Controls.Add(name_case);

                Label name_case_price = new Label();
                name_case_price.Text = Convert.ToString(case_price);
                name_case_price.Location = new System.Drawing.Point(1000, 167);
                panel7.Controls.Add(name_case_price);

                // Вывод видеокарты
                PictureBox videocard_pic = new PictureBox();
                videocard_pic.Size = new Size(175, 77);
                videocard_pic.Location = new Point(0, 228); //y+76
                videocard_pic.Image = new Bitmap(Directory.GetCurrentDirectory() + "\\image\\" + videocard_picture + ".jpg");
                videocard_pic.SizeMode = PictureBoxSizeMode.Zoom;
                panel7.Controls.Add(videocard_pic);

                Label name_videocard = new Label();
                name_videocard.Text = current_videocard;
                name_videocard.Location = new System.Drawing.Point(181, 243);
                panel7.Controls.Add(name_videocard);

                Label name_videocard_price = new Label();
                name_videocard_price.Text = Convert.ToString(videocard_price);
                name_videocard_price.Location = new System.Drawing.Point(1000, 243);
                panel7.Controls.Add(name_videocard_price);

                //вывод озу
                Label name_ram = new Label();
                name_ram.Text = current_ram;
                name_ram.Location = new System.Drawing.Point(181, 319);
                panel7.Controls.Add(name_ram);

                Label name_ram_price = new Label();
                name_ram_price.Text = Convert.ToString(ram_price);
                name_ram_price.Location = new System.Drawing.Point(1000, 319);
                panel7.Controls.Add(name_ram_price);

                PictureBox ram_pic = new PictureBox();
                ram_pic.Size = new Size(175, 77);
                ram_pic.Location = new Point(0, 304); //y+76
                ram_pic.Image = new Bitmap(Directory.GetCurrentDirectory() + "\\image\\" + ram_picture + ".jpg");
                ram_pic.SizeMode = PictureBoxSizeMode.Zoom;
                panel7.Controls.Add(ram_pic);

                //вывод кулера
                Label name_cooler = new Label();
                name_cooler.Text = current_cooler;
                name_cooler.Location = new System.Drawing.Point(181, 395);
                panel7.Controls.Add(name_cooler);

                Label name_cooler_price = new Label();
                name_cooler_price.Text = Convert.ToString(cooler_price);
                name_cooler_price.Location = new System.Drawing.Point(1000, 395);
                panel7.Controls.Add(name_cooler_price);

                PictureBox cooler_pic = new PictureBox();
                cooler_pic.Size = new Size(175, 77);
                cooler_pic.Location = new Point(0, 380); //y+76
                cooler_pic.Image = new Bitmap(Directory.GetCurrentDirectory() + "\\image\\" + cooler_picture + ".jpg");
                cooler_pic.SizeMode = PictureBoxSizeMode.Zoom;
                panel7.Controls.Add(cooler_pic);

                //вывод жд
                Label name_storage = new Label();
                name_storage.Text = current_storage;
                name_storage.Location = new System.Drawing.Point(181, 471);
                panel7.Controls.Add(name_storage);

                Label name_storage_price = new Label();
                name_storage_price.Text = Convert.ToString(storage_price);
                name_storage_price.Location = new System.Drawing.Point(1000, 471);
                panel7.Controls.Add(name_storage_price);

                PictureBox storage_pic = new PictureBox();
                storage_pic.Size = new Size(175, 77);
                storage_pic.Location = new Point(0, 456); //y+76
                storage_pic.Image = new Bitmap(Directory.GetCurrentDirectory() + "\\image\\" + storage_picture + ".jpg");
                storage_pic.SizeMode = PictureBoxSizeMode.Zoom;
                panel7.Controls.Add(storage_pic);

                //вывод БП
                Label name_ps = new Label();
                name_ps.Text = current_ps;
                name_ps.Location = new System.Drawing.Point(181, 543);
                panel7.Controls.Add(name_ps);

                Label name_ps_price = new Label();
                name_ps_price.Text = Convert.ToString(ps_price);
                name_ps_price.Location = new System.Drawing.Point(1000, 543);
                panel7.Controls.Add(name_ps_price);

                PictureBox ps_pic = new PictureBox();
                ps_pic.Size = new Size(175, 77);
                ps_pic.Location = new Point(0, 532); //y+76
                ps_pic.Image = new Bitmap(Directory.GetCurrentDirectory() + "\\image\\" + ps_picture + ".jpg");
                ps_pic.SizeMode = PictureBoxSizeMode.Zoom;
                panel7.Controls.Add(ps_pic);

                //форматирование вывода 
                name_cpu.AutoSize = name_videocard.AutoSize = name_case.AutoSize = name_cooler.AutoSize = name_mb.AutoSize = name_ram.AutoSize = name_storage.AutoSize = name_ps.AutoSize = true;


                Label summary_price = new Label();
                summary_price.Text = "Итоговая стоимость:\n" + Convert.ToString(cpu_price + videocard_price + case_price + motherboard_price + ram_price + cooler_price + storage_price + ps_price);
                summary_price.Location = new System.Drawing.Point(850, 630);
                panel7.Controls.Add(summary_price);
                summary_price.AutoSize = true;


                int coord = 0;

                if (cpu_tdp + videocard_tdp >= ps_tdp)
                {
                    Label comment_cpu_video_check = new Label();
                    comment_cpu_video_check.Text = "Мощность блока питания недостаточно.";
                    comment_cpu_video_check.Location = new System.Drawing.Point(50, 700 + coord);
                    panel7.Controls.Add(comment_cpu_video_check);
                    comment_cpu_video_check.AutoSize = true;
                    coord += 40;
                }

                if (cpu_socket != motherboard_socket)
                {
                    Label comment_mb_socket_check = new Label();
                    comment_mb_socket_check.Text = "Несовместимый сокет материнской платы.";
                    comment_mb_socket_check.Location = new System.Drawing.Point(50, 700 + coord);
                    panel7.Controls.Add(comment_mb_socket_check);
                    comment_mb_socket_check.AutoSize = true;
                    coord += 40;
                }
                if (motherboard_type_ram != ram_type)
                {
                    Label comment_mb_ram_check = new Label();
                    comment_mb_ram_check.Text = "Тип оператиной памяти не поддерживаются материнской платой.";
                    comment_mb_ram_check.Location = new System.Drawing.Point(50, 700 + coord);
                    panel7.Controls.Add(comment_mb_ram_check);
                    comment_mb_ram_check.AutoSize = true;
                    coord += 40;
                }
                if (motherboard_ram_min >= ram_mhz || ram_mhz >= motherboard_ram_max)
                {
                    Label comment_min_max_ram = new Label();
                    comment_min_max_ram.Text = "Частоты оператиной памяти не поддерживаются материнской платой.";
                    comment_min_max_ram.Location = new System.Drawing.Point(50, 700 + coord);
                    panel7.Controls.Add(comment_min_max_ram);
                    comment_min_max_ram.AutoSize = true;
                    coord += 40;
                }
                if (motherboard_type != case_type)
                {
                    Label comment_min_max_ram = new Label();
                    comment_min_max_ram.Text = "Размеры корпуса слишком малы для установки материнской платы.";
                    comment_min_max_ram.Location = new System.Drawing.Point(50, 700 + coord);
                    panel7.Controls.Add(comment_min_max_ram);
                    comment_min_max_ram.AutoSize = true;
                }
                current_cpu = current_case = current_videocard = current_ram = current_ps = current_storage = current_motherboard = current_cooler = "";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri((Application.StartupPath + "\\Ресурсы\\cpu.html"));
        }

        private void button11_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri((Application.StartupPath + "\\Ресурсы\\motherboard.html"));
        }

        private void button12_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri((Application.StartupPath + "\\Ресурсы\\PC-case.html"));

        }

        private void button13_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri((Application.StartupPath + "\\Ресурсы\\videocard.html"));

        }

        private void button14_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri((Application.StartupPath + "\\Ресурсы\\ram.html"));

        }

        private void button15_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri((Application.StartupPath + "\\Ресурсы\\PC-cooler.html"));

        }

        private void button16_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri((Application.StartupPath + "\\Ресурсы\\storage.html"));
        }

        private void button17_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri((Application.StartupPath + "\\Ресурсы\\powersupply.html"));
        }

        private void button18_Click(object sender, EventArgs e)
        {
            webBrowser2.Url = new Uri((Application.StartupPath + "\\Ресурсы\\low-price.html"));
        }

        Test[] massive;
        RadioButton rb1 = new RadioButton();
        RadioButton rb2 = new RadioButton();
        RadioButton rb3 = new RadioButton();
        RadioButton rb4 = new RadioButton();
        Label lb = new Label();
        Button bt = new Button();
        RichTextBox rt = new RichTextBox();
        WebBrowser rt2 = new WebBrowser();
        string Pathname = "";
        int kolvoprosov = 0;
        int nomervoprosa = 0;
        int kolvovernih = 0;
        int FC = 1;

        private void button21_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
                Pathname = btn.Text;
            string Path = Application.StartupPath + "\\Ресурсы\\"+Pathname+ ".txt";
            panel10.Controls.Add(rb1);
            rb1.AutoSize = true;
            rb1.Location = new Point(25, 61);
            panel10.Controls.Add(rb2);
            rb2.AutoSize = true;
            rb2.Location = new Point(25, 95);
            panel10.Controls.Add(rb3);
            rb3.AutoSize = true;
            rb3.Location = new Point(25, 129);
            panel10.Controls.Add(rb4);
            rb4.AutoSize = true;
            rb4.Location = new Point(25, 163);
            lb.AutoSize = true;
            lb.Location = new Point(20, 20);
            panel10.Controls.Add(lb);
            bt.Location = new Point(25, 214);
            bt.Size = new Size(100, 39);
            panel10.Controls.Add(bt);
            bt.Text = "Далее";
            if (FC == 1)
            {
                bt.Click += Bt_Click;
                FC = 0;
            } 
            rt.Location = new Point(3, 285);
            rt.Size = new Size (547, 211);
            rt.Text = "";
            panel10.Controls.Add(rt);
            rt2.Location = new Point(601, 251);
            rt2.Size = new Size(532, 248);
            panel10.Controls.Add(rt2);
            rt2.Url = new Uri(Application.StartupPath + "\\Ресурсы\\история.html");
            kolvoprosov = 0;
            nomervoprosa = 0;
            kolvovernih = 0;
            FileStream sf = new FileStream(Path, FileMode.Open);
            StreamReader sr = new StreamReader(sf);
            sr.ReadLine();
            kolvoprosov = Convert.ToInt32(sr.ReadLine());
            massive = new Test[kolvoprosov];
            for (int key = 0; key < kolvoprosov; key++)
            {
                massive[key] = new Test();
            }
            string strfile = sr.ReadLine();
            int index = 0;
            while (strfile != null)
            {
                massive[index].Qwestion = strfile;

                for (int i = 0; i < 4; i++)
                {
                    strfile = sr.ReadLine();
                    string flag = strfile.Substring(0, 1);
                    if (flag == "+")
                    {
                        massive[index].Otvet[i] = true;
                        massive[index].Verniy_Otvet = i;
                    }
                    else
                    {
                        massive[index].Otvet[i] = false;
                    }
                    massive[index].Varianty_otvetov[i] = strfile.Substring(1);
                }
                index++;
                strfile = sr.ReadLine();
            }
            sr.Close();
            sf.Close();
            lb.Text = massive[0].Qwestion;
            rb1.Text = massive[0].Varianty_otvetov[0];
            rb2.Text = massive[0].Varianty_otvetov[1];
            rb3.Text = massive[0].Varianty_otvetov[2];
            rb4.Text = massive[0].Varianty_otvetov[3];
        }

        private void Bt_Click(object sender, EventArgs e)
        {

            int Num = -1;
            if (rb1.Checked == true) Num = 1;
            if (rb2.Checked == true) Num = 2;
            if (rb3.Checked == true) Num = 3;
            if (rb4.Checked == true) Num = 4;
            massive[nomervoprosa].User_Otvet = Num - 1;

            if (massive[nomervoprosa].Otvet[Num - 1] == true)
            {
                rt.Text += "Вопрос №" + (nomervoprosa + 1) + ": верно\n";
                kolvovernih++; // увеличиваем, число верных ответов
            }
            else
            {
                rt.Text += "Вопрос №" + (nomervoprosa + 1) + ": не верно\n";
            }

            // проверка, был ли это последний вопрос?
            if (nomervoprosa == kolvoprosov - 2)
            {
                MessageBox.Show("Количество верных ответов: " + Convert.ToString(kolvovernih) + " из " + kolvoprosov + "\nВыполнено: " + (kolvovernih * 100) / kolvoprosov + " %", "Тест завершен!");

                int rate = 0;
                if (((kolvovernih * 100) / kolvoprosov) >= 70)
                {
                    MessageBox.Show("Вы молодец! Отметка 5");
                    rate = 5;
                }
                else if (((kolvovernih * 100) / kolvoprosov) >= 60)
                {
                    MessageBox.Show("Хорошо справились! Отметка 4");
                    rate = 4;
                }
                else if (((kolvovernih * 100) / kolvoprosov) >= 50)
                {
                    MessageBox.Show("Можно было и лучше. Отметка 3");
                    rate = 3;
                } 
                else {
                    MessageBox.Show("Очень плохо. Отметка 2");
                    rate = 2;
                }
                string pathResult = Application.StartupPath + "\\Ресурсы\\история.html";
                using (StreamWriter w = new StreamWriter(pathResult, true, Encoding.Default))
                {
                    w.WriteLine("<br><br>Номер теста: {0}, Оценка: {1}, Дата тестирования: {2} <br>", Pathname, rate, DateTime.Now);
                }
                panel10.Controls.Clear();
                return;
            } else { 
            nomervoprosa++;
            lb.Text = massive[nomervoprosa].Qwestion;
            // вывод вариантов ответа
                rb1.Text = massive[nomervoprosa].Varianty_otvetov[0];
                rb2.Text = massive[nomervoprosa].Varianty_otvetov[1];
                rb3.Text = massive[nomervoprosa].Varianty_otvetov[2];
                rb4.Text = massive[nomervoprosa].Varianty_otvetov[3];
            }
        }

        public class Test
        {
            // объект класса содержит:
            public string Qwestion; // вопрос
            public string[] Varianty_otvetov = new string[4]; // четыре вырианта ответа
            public bool[] Otvet = new bool[4]; // массив, содержащий праильный вариант-true, неправильные-false
            public int Verniy_Otvet; // Верный ответ
            public int User_Otvet; // ответ пользователя
        }
        int ChekedTest1()
        {
            if (rb1.Checked == true) return 1;
            if (rb2.Checked == true) return 2;
            if (rb3.Checked == true) return 3;
            return 4;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            webBrowser2.Url = new Uri((Application.StartupPath + "\\Ресурсы\\mid-price.html"));
        }

        private void button20_Click(object sender, EventArgs e)
        {
            webBrowser2.Url = new Uri((Application.StartupPath + "\\Ресурсы\\hign-price.html"));
        }
    }
}
