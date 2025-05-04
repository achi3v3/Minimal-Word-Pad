using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;
namespace MiniWordPad
{
    public partial class Form1: Form
    {
        private string currentFilePath = null;

        public Form1()
        {
            InitializeComponent();
            Text = "Minimal WordPad";
            UpdateStatus("Готов");

            // Повторное выделение найденного текста при возвращении фокуса окну
            this.Activated += (s, e) =>
            {
                if (foundPositions.Count > 0)
                    HighlightFoundText();
            };


            // Скрытие панелей инструментов по умолчанию
            panelSearch.Width = 0;
            panelReplace.Width = 0;
        }

        // Диалог выбора шрифта и цвета текста
        private void fontDefaultToolStripButton_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.ShowColor = true;

            if (contentRichTextBox.SelectionFont != null)
            {
                fontDialog.Font = contentRichTextBox.SelectionFont;
                fontDialog.Color = contentRichTextBox.SelectionColor;
            }

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                contentRichTextBox.SelectionFont = fontDialog.Font;
                contentRichTextBox.SelectionColor = fontDialog.Color;
            }
        }

        private void contentRichTextBox_TextChanged(object sender, EventArgs e)
        {
            searchText = "";
            foundPositions.Clear();
            currentPosition = 0;
            UpdateStatus();
        }
        private void contentRichTextBox_SelectionChanged(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        // Обновление строки состояния: сообщение, позиция курсора и число символов
        private void UpdateStatus(string message = "")
        {
            if (!string.IsNullOrEmpty(message))
                statusLabel.Text = message;

            int index = contentRichTextBox.SelectionStart;
            int line = contentRichTextBox.GetLineFromCharIndex(index);
            int column = index - contentRichTextBox.GetFirstCharIndexOfCurrentLine();
            int charCount = contentRichTextBox.TextLength;

            cursorPoisitionLabel.Text = $"Строка: {line + 1}, Столбец: {column + 1}";
            charCountLabel.Text = $"Символов: {charCount}";
        }
        // ===== Функции ФАЙЛ =====
        private void ResetFormatting()
        {
            contentRichTextBox.SelectionFont = new Font("Times New Roman", 12f);
            contentRichTextBox.SelectionColor = Color.Black;

            contentRichTextBox.SelectionAlignment = HorizontalAlignment.Left;

            searchBox.Text = "";
            textBoxOn.Text = "";
            textBoxWhat.Text = "";
        }
   
        // Создание нового документа с проверкой на несохранённые изменения
        private void NewFile()
        {
            if (contentRichTextBox.TextLength > 0)
            {
                var result = MessageBox.Show("Сохранить изменения?", "Minimal WordPad",
                                              MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    SaveFile();
                else if (result == DialogResult.Cancel)
                    return;
            }
            contentRichTextBox.Clear();
            currentFilePath = null;
            Text = "Minimal WordPad - Новый документ";
            ResetFormatting();
        }
        // Открытие файла .txt или .rtf
        private void OpenFile()
        {
            if (contentRichTextBox.TextLength > 0 && contentRichTextBox.Modified)
            {
                var result = MessageBox.Show("Сохранить изменения в текущем документе?", "Minimal WordPad",
                                              MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    SaveFile();
                else if (result == DialogResult.Cancel)
                    return;
            }

            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Текстовые файлы (*.txt)|*.txt|RTF файлы (*.rtf)|*.rtf";
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ResetFormatting(); // сброс форматирования перед загрузкой

                        if (openDialog.FileName.EndsWith(".rtf"))
                            contentRichTextBox.LoadFile(openDialog.FileName);
                        else
                            contentRichTextBox.LoadFile(openDialog.FileName, RichTextBoxStreamType.PlainText);

                        currentFilePath = openDialog.FileName;
                        Text = $"Minimal WordPad - {Path.GetFileName(currentFilePath)}";
                        UpdateStatus("Файл открыт");
                        contentRichTextBox.Modified = false; // сброс флага изменений
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
                    }
                }
            }
        }
        // Сохранение текущего файла
        private void SaveFile()
        {
            if (string.IsNullOrEmpty(currentFilePath))
                SaveFileAs(); // если нет пути — вызов "Сохранить как"
            else
            {
                try
                {
                    if (currentFilePath.EndsWith(".rtf"))
                        contentRichTextBox.SaveFile(currentFilePath);
                    else
                        contentRichTextBox.SaveFile(currentFilePath, RichTextBoxStreamType.PlainText);

                    UpdateStatus("Файл сохранен");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
                }
            }
        }
        private void SaveFileAs()
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Текстовые файлы (*.txt)|*.txt|RTF файлы (*.rtf)|*.rtf";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveDialog.FileName;
                    SaveFile();
                    Text = $"Minimal WordPad - {Path.GetFileName(currentFilePath)}";
                }
            }
        }

        // ===== Обработчики меню =====
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile();
            searchBox.Text = "";
            textBoxOn.Text = "";
            textBoxWhat.Text = "";
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
            searchBox.Text = "";
            textBoxOn.Text = "";
            textBoxWhat.Text = "";
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contentRichTextBox.TextLength > 0)
            {
                var result = MessageBox.Show("Сохранить изменения перед выходом?", "Minimal WordPad",
                                            MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    SaveFile();
                else if (result == DialogResult.Cancel)
                    return;
            }
            Application.Exit();
        }

        // ===== Панель инструментов =====

        private void alignLeftToolStripButton_Click(object sender, EventArgs e)
        {
            contentRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
            UpdateStatus("Выравнивание по левому краю");

        }

        private void alignCenterToolStripButton_Click(object sender, EventArgs e)
        {
            contentRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
            UpdateStatus("Выравнивание по центру");
        }

        private void alignRightToolStripButton_Click(object sender, EventArgs e)
        {
            contentRichTextBox.SelectionAlignment = HorizontalAlignment.Right;
            UpdateStatus("Выравнивание по правому краю");
        }

        // Копирование текста (с RTF и PlainText)
        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            if (contentRichTextBox.SelectionLength > 0)
            {
                try
                {
                    DataObject data = new DataObject();
                    data.SetText(contentRichTextBox.SelectedText);
                    if (contentRichTextBox.SelectedRtf != null)
                        data.SetData(DataFormats.Rtf, contentRichTextBox.SelectedRtf);

                    Clipboard.Clear();
                    Clipboard.SetDataObject(data, true);
                    UpdateStatus("Текст скопирован");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка копирования: {ex.Message}", "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Выделите текст для копирования", "Уведомление");
            }
        }


        // Вставка текста с проверкой формата
        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Clipboard.ContainsText() || Clipboard.ContainsText(TextDataFormat.Rtf))
                {
                    if (Clipboard.ContainsText(TextDataFormat.Rtf))
                        contentRichTextBox.Paste(DataFormats.GetFormat(DataFormats.Rtf));
                    else
                        contentRichTextBox.Paste(DataFormats.GetFormat(DataFormats.Text));

                    UpdateStatus("Текст вставлен");
                }
                else
                {
                    MessageBox.Show("Буфер обмена не содержит текста", "Уведомление");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка вставки: {ex.Message}", "Ошибка");
            }
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            if (contentRichTextBox.SelectionLength > 0)
            {
                try
                {
                    DataObject data = new DataObject();

                    data.SetText(contentRichTextBox.SelectedText);
                    if (contentRichTextBox.SelectedRtf != null)
                    {
                        data.SetData(DataFormats.Rtf, contentRichTextBox.SelectedRtf);
                    }

                    Clipboard.Clear();
                    Clipboard.SetDataObject(data, true);

                    contentRichTextBox.SelectedText = "";

                    UpdateStatus("Текст вырезан");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при вырезании: {ex.Message}", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Выделите текст для вырезания", "Инфо",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private string searchText = "";
        private int currentPosition = 0;
        private List<int> foundPositions = new List<int>();
        private Form searchForm;

        private void searchToolStripButton_Click(object sender, EventArgs e)
        {
            if (panelSearch.Width < 100)
            {
                panelSearch.Width = 150;
                panelReplace.Width = 0;

            }
            else
            {
                panelSearch.Width = 0;
            }
        }
        private void ReplaceToolStripButton_Click(object sender, EventArgs e)
        {
            if (panelReplace.Width < 100)
            {
                panelReplace.Width = 150;
                panelSearch.Width = 0;
            }
            else
            {
                panelReplace.Width = 0;
            }
        }
        private void buttonNext_Click(object sender, EventArgs e)
        {
            FindNext(searchBox.Text);
        }
        private void buttonPrev_Click(object sender, EventArgs e)
        {
            FindPrevious(searchBox.Text);
        }
        private void buttonNextReplace_Click(object sender, EventArgs e)
        {
            FindNext(textBoxWhat.Text);
        }
        private void buttonPrevReplace_Click(object sender, EventArgs e)
        {
            FindPrevious(textBoxWhat.Text);
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            contentRichTextBox.Redo();
            UpdateStatus("Действие вперёд");
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            contentRichTextBox.Undo();
            UpdateStatus("Вернуть действие");
        }

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            ReplaceCurrent();
        }
        private void buttonReplaceAll_Click(object sender, EventArgs e)
        {
            ReplaceAll();
        }


        /*
        Метод FindNext ищет следующее вхождение строки `searchText` в RichTextBox.
        Поиск начинается с текущей позиции курсора и идёт вперёд до конца текста.
        Если ничего не найдено — отображается сообщение.
        */
        private void FindNext(string text)
        {
            if (text != searchText)
            {
                // Если текст поиска изменился, ищем заново
                searchText = text;
                currentPosition = 0;
                FindAllOccurrences();
            }
            if (foundPositions.Count == 0)
            {
                MessageBox.Show("Текст не найден!");
                return;
            }

             currentPosition += 1;
            if (currentPosition >= foundPositions.Count)
            {
                currentPosition = 0;
            }

            HighlightFoundText();
            contentRichTextBox.Focus();

            }
        /*
        Поиск уже предыдущего вхождения текста, аналогично методу FindNext
        */
        private void FindPrevious(string text)
        {
            if (text != searchText)
            {
                searchText = text;
                currentPosition = 0;
                FindAllOccurrences();
            }

            if (foundPositions.Count == 0)
            {
                MessageBox.Show("Текст не найден!");
                return;
            }

            currentPosition -= 1;
            if (currentPosition < 0)
            {
                currentPosition = foundPositions.Count - 1;
            }

            HighlightFoundText();
            contentRichTextBox.Focus();

        }
        /*
        Поиск всех вхождений
        */
        private void FindAllOccurrences()
        {
            foundPositions.Clear(); // Очищаем список ранее найденных позиций (чтобы не мешали новые результаты)
            int index = 0;

            // Пока не достигнут конец текста
            while (index < contentRichTextBox.Text.Length)
            {
                index = contentRichTextBox.Find(
                    searchText,                  // Ищем строку поиска
                    index,                       // начиная с текущего индекса
                    RichTextBoxFinds.None        // без специальных параметров (например, регистрозависимость и пр.)
                );

                if (index == -1) break; // Если ничего не найдено — выходим из цикла

                foundPositions.Add(index); // Сохраняем позицию найденного вхождения
                index += searchText.Length; // Продвигаем указатель дальше, чтобы не зациклиться
            }
        }
        /*
        Выделение найденного текста
        */
        private void HighlightFoundText()
        {
            contentRichTextBox.SelectionLength = 0; // Убираем любое текущее выделение

            // Если есть найденные позиции и текущая позиция в допустимых пределах
            if (foundPositions.Count > 0 && currentPosition < foundPositions.Count)
            {
                contentRichTextBox.SelectionStart = foundPositions[currentPosition]; // Устанавливаем курсор на нужную позицию
                contentRichTextBox.SelectionLength = searchText.Length;              // Выделяем текст той же длины, что и запрос

                contentRichTextBox.ScrollToCaret(); // Прокручиваем к выделенному тексту
                contentRichTextBox.Refresh();       // Обновляем отображение (иногда требуется для применения визуальных изменений)
            }
        }

        // Замена текущего найденного текста
        private void ReplaceCurrent()
        {
            string search = textBoxWhat.Text;
            string replace = textBoxOn.Text;

            if (string.IsNullOrEmpty(search)) return;

            if (search != searchText || foundPositions.Count == 0)
            {
                searchText = search;
                currentPosition = 0;
                foundPositions.Clear();
                FindAllOccurrences();
            }

            if (foundPositions.Count == 0) return;

            int position = foundPositions[currentPosition];

            contentRichTextBox.SelectionStart = position;
            contentRichTextBox.SelectionLength = search.Length;
            contentRichTextBox.SelectedText = replace;

            UpdateStatus($"Заменено {search} - {replace}");

            foundPositions.Clear();
            FindAllOccurrences();

            currentPosition++;
            if (currentPosition >= foundPositions.Count) currentPosition = 0;

            HighlightFoundText();
        }
        private void ReplaceAll()
        {
            if (!string.IsNullOrEmpty(textBoxWhat.Text))
            {
                ReplaceAllOccurrences(textBoxWhat.Text, textBoxOn.Text);
            }
        }
        
        /*
        Метод ReplaceAllOccurrences выполняет замену всех вхождений строки `searchText` на строку `replaceText`
        в документе, представленном в RichTextBox (contentRichTextBox).
        */
        private void ReplaceAllOccurrences(string searchText, string replaceText)
        {
            // Проверяем, что строка для поиска не пуста
            if (string.IsNullOrEmpty(searchText)) return;

            // Временно приостанавливаем обновление визуального интерфейса,
            // чтобы избежать мигания и повысить производительность
            contentRichTextBox.SuspendLayout();

            int replaceCount = 0; // Счётчик замен
            int index = 0;        // Текущий индекс поиска

            // Пока не дошли до конца текста
            while (index < contentRichTextBox.TextLength)
            {
                // Ищем вхождение текста с текущей позиции
                index = contentRichTextBox.Find(
                    searchText,
                    index,
                    RichTextBoxFinds.None
                );

                // Если больше вхождений нет — выходим
                if (index == -1) break;

                // Выделяем найденный текст
                contentRichTextBox.SelectionStart = index;
                contentRichTextBox.SelectionLength = searchText.Length;

                // Заменяем выделенный текст на новый
                contentRichTextBox.SelectedText = replaceText;

                replaceCount++; // Увеличиваем счётчик замен

                // Переходим на следующую позицию после вставленного текста
                index += replaceText.Length;
            }

            // Возобновляем обновление визуального интерфейса
            contentRichTextBox.ResumeLayout();

            // Обновляем статусную строку с информацией о количестве замен
            UpdateStatus($"Заменено {replaceCount} вхождений");
        }

        /*
        Обработка закрытия окна 
        */
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (contentRichTextBox.TextLength > 0)
            {
                var result = MessageBox.Show("Сохранить изменения перед выходом?", "Minimal WordPad",
                                            MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    SaveFile();
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }
    }
}