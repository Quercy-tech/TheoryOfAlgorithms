using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Laba4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private List<int> studentsList = new List<int>() { 3, 2, 1 };

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public int counter = 0;
        public int dequeCounter = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            // Sample usage

            int queueType = 0; // FIFO queue
            int studentsNumber = studentsList.Count;

            Algorithm algorithm = new Algorithm();
            List<string> result = algorithm.TakingExam(studentsList, queueType, studentsNumber);

            string s = richTextBox3.Text;

            if (counter < result.Count)
                richTextBox1.Text += (result[counter] + "\n");

            if (counter >= 2 * studentsNumber && counter % 2 == 0 && s.Length >= 0)
            {
                s = s.Substring(0, s.Length - 2);
                richTextBox3.Text = s;
            }

            counter += 1;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // Sample usage

            int queueType = 1; // FIFO queue
            int studentsNumber = studentsList.Count;

            Algorithm algorithm = new Algorithm();
            List<string> result = algorithm.TakingExam(studentsList, queueType, studentsNumber);

            string s = richTextBox3.Text;

            if (counter < result.Count)
                richTextBox1.Text += (result[counter] + "\n");


                if (counter >= 2 * studentsNumber && counter % 2 == 0 && s.Length >= 0)
                {
                    s = s.Substring(0, s.Length - 2);
                    richTextBox3.Text = s;
                }

            counter += 1;
            



        }

        class Algorithm
        {
            public List<string> TakingExam(List<int> studentsList, int queueType, int studentsNumber)
            {
                Queue queue = new Queue(studentsNumber); //Initialising
                PointedQueue pointedQueue = new PointedQueue();
                List<string> text = new List<string>();

                int charNumber = 0; //Outputing the queue
                foreach (int student in studentsList)
                {
                    foreach (char ch in student.ToString())
                    {
                        charNumber++; ;
                    }
                    charNumber++; ;
                }
                char[] queueLineList = new char[charNumber];
                int i = 0;
                foreach (int student in studentsList)
                {
                    foreach (char ch in student.ToString())
                    {
                        queueLineList[i] = ch;
                        i++;
                    }
                    queueLineList[i] = '\t';
                    i++;
                }
                String queueLine = new String(queueLineList);

                try //Do the task and count the time
                {
                    FillingQueue();
                    OutputExam();
                    FillingQueue();
                    OutputExam(true);
                }
                catch (Exception ex) //Stop the program if necessary
                {
                    text.Add(ex.Message);
                    return text;
                }


                return text;





                void FillingQueue()
                {
                    foreach (int student in studentsList)
                    {
                        if (queueType == 1)
                        {
                            queue.Enqueue(student);
                        }
                        else
                        {
                            pointedQueue.Enqueue(student);
                        }
                    }

                }

                

                void OutputExam(bool practise = false)
                {
                    int taskNumber = 1;
                    string line = "";
                    int currentStudent = (queueType == 1) ? queue.Dequeue() : pointedQueue.Dequeue();
                    while ((queueType == 1) ? !queue.IsEmpty() : !pointedQueue.IsEmpty())
                    {
                        taskNumber += 1;

                        line = (practise) ? "Practical work №" + (taskNumber - 1) + " and №" + (taskNumber) + " for" :
                            "Theoretical survey №" + (taskNumber - 1) + " and №" + (taskNumber) + " for";


                        int nextStudent = (queueType == 1) ? queue.Dequeue() : pointedQueue.Dequeue();
                        
                        text.Add(line + " " + currentStudent);
                        text.Add("Transfering part of task №" + taskNumber + " for student " + nextStudent);
                        currentStudent = nextStudent;
                    }
                    taskNumber += 1;
                    line = (practise) ? "Practical work №" + (taskNumber - 1) + " and №" + (taskNumber) + " for" :
                            "Theoretical survey №" + (taskNumber - 1) + " and №" + (taskNumber) + " for";
                    text.Add(line + " " + currentStudent);
                }

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text = "";
            richTextBox3.Text = "";
            // Clear the existing list in case of multiple modifications
            studentsList.Clear();

            // Get the text from the textbox
            string textBoxContent = richTextBox2.Text;

            // Split the text by comma or any other delimiter you prefer
            string[] studentNumbers = textBoxContent.Split(',');

            // Try converting each string to int and add to the list
            foreach (string number in studentNumbers)
            {
                int studentNumber;
                if (int.TryParse(number, out studentNumber) && studentNumber > 0)
                {
                    studentsList.Add(studentNumber);
                    richTextBox3.Text += (number + "\n");
                }
                else
                {
                    richTextBox2.Text = "";
                    MessageBox.Show("Enter valid input!");
                    return;

                }
            }
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }

    class Queue
    {
        int[] array;
        int head;
        int tail;
        int size;
        int capacity;
        public Queue(int capacity)
        {
            this.capacity = capacity;
            this.array = new int[capacity];
            this.head = 0;
            this.tail = -1;
            this.size = 0;
        }
        public void Enqueue(int item)
        {
            if (this.size == this.capacity)
            {
                throw new Exception("Queue overflow");
            }
            this.tail++;
            if (this.tail == this.capacity)
            {
                this.tail = 0;
            }
            this.array[this.tail] = item;
            this.size++;
        }
        public int Dequeue()
        {
            if (this.size == 0)
            {
                throw new Exception("Queue is empty");
            }
            int item = this.array[this.head];
            this.head++;
            if (this.head == this.capacity)
            {
                this.head = 0;
            }
            this.size--;
            return item;
        }
        public bool IsEmpty()
        {
            return this.size == 0;
        }

    }




    class PointedQueue
    {
        class QueueItem
        {
            public int item;
            public QueueItem next;
            public QueueItem(int item)
            {
                this.item = item;
            }
        }
        QueueItem head;
        QueueItem tail;

        public void Enqueue(int item)
        {
            QueueItem newItem = new QueueItem(item);
            if (this.tail == null)
            {
                this.head = newItem;
            }
            else
            {
                this.tail.next = newItem;
            }
            this.tail = newItem;
        }

        public int Dequeue()
        {

            int item = this.head.item;
            head = head.next;
            if (head == null)
            {
                tail = null;
            }
            return item;
        }
        public bool IsEmpty()
        {
            return head == null;
        }

    }

}
