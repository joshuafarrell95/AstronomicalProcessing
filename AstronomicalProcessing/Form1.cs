﻿// Team: Joshua and Rhys
// Date: 07/04/2022
// Description

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AstronomicalProcessing
{
    public partial class frmAstronomicalProcessing : Form
    {
        // Create neutrinoData array with length 24
        static int arrayLength = 24;
        int[] neutrinoData = new int[arrayLength];
        public frmAstronomicalProcessing() {
            InitializeComponent();
        }
        
        // Fill the array with random values, to simulate the data
        public void FillArray() {
            Random rand = new Random();
            for (int i = 0; i < arrayLength; i++) {
                neutrinoData[i] = rand.Next(10, 99);
            }
        }

        // Clear the list box and reprint the array
        public void RefreshArray() {
            lstArray.Items.Clear();
            for (int i = 0; i < arrayLength; i++) {
                lstArray.Items.Add(neutrinoData[i]);
            }
        }

        // Check and re-randomise duplicate data in array 
        public void CheckDuplicate()
        {
            int numLength = arrayLength;
            bool flag = true;
            for (int i = 1; (i <= (numLength - 1)) && flag; i++)
            {
                flag = false;
                for (int j = 0; j < (numLength - 1); j++)
                {
                    if (neutrinoData[j + 1] == neutrinoData[j])
                    {
                        Random rand = new Random();
                        neutrinoData[j] = rand.Next(10, 99);
                        flag = true;
                    }
                }
            }
        }

        // Perform the FillArray() and RefreshArray() methods when btnRandomise is clicked
        private void RandomiseArray(object sender, EventArgs e)
        {
            FillArray();
            RefreshArray();
            EnableSortButton();
        }

        // Sort the array using the bubble sorting algorithm when btnSort is clicked
        private void BubbleSort(object sender, EventArgs e) {
            int numLength = arrayLength;
            bool flag = true;
            for (int i = 1; (i <= (numLength - 1)) && flag; i++) {
                flag = false;
                for (int j = 0; j < (numLength - 1); j++) {
                    if (neutrinoData[j + 1] < neutrinoData[j]) {
                        int temp = neutrinoData[j];
                        neutrinoData[j] = neutrinoData[j + 1];
                        neutrinoData[j + 1] = temp;
                        flag = true;
                    }
                }
            }
            RefreshArray();
            CheckDuplicate();
            EnableSearchButton();
            EnableEditButton();
        }

        // Perform a Binary Search when txtSearch has an integer value and btnSearch is clicked
        private void BinarySearch(object sender, EventArgs e)
        {
            // Locally declare minimum and maximum array values
            int min = 0;
            int max = arrayLength - 1;
            if (!(Int32.TryParse(txtSearch.Text, out int target)))
            {
                MessageBox.Show("You must enter an integer");
                return;
            }

            while (min <= max)
            {
                // Write the mean of min and max to mid
                int mid = (min + max) / 2;

                if (target == neutrinoData[mid])
                {
                    MessageBox.Show(target + " Found at index " + mid);
                    return;
                }
                else if (neutrinoData[mid] >= target)
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            MessageBox.Show(target + " not found in array");
        }

        // Checks both text boxes if there are any non-integer characters
        private void IntegerCheck(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar; 
            if (!char.IsDigit(ch) && !char.IsControl(ch))
            {
                // If the test fails, handle the event and don't input non-integer character
                e.Handled = true;
            }
        }
        
        // Edit the selected item within the array and then sort the array
        private void EditData(object sender, EventArgs e)
        {
            if (!(Int32.TryParse(txtEdit.Text, out int editData)))
            {
                MessageBox.Show("You must enter an integer");
                return;
            }
            else if (lstArray.SelectedItem == null)
            {
                MessageBox.Show("You must select an item in the array");
                return;
            }
            else
            {
                string currentItem = lstArray.SelectedItem.ToString();
                int index = lstArray.FindString(currentItem);
                neutrinoData[index] = editData;
                BubbleSort(this, e);
            }
        }

        // Check if the Enter key is pressed in both text boxes, then run the correct method
        private void EnterKeyCheck(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSearch.Focused)
                {
                    BinarySearch(this, e);
                }
                else if (txtEdit.Focused)
                {
                    EditData(this, e);
                }
                else
                {
                    MessageBox.Show("Please enter an integer in the box");
                }
            }
        }

        // Enable Sort button
        private void EnableSortButton()
        {
            btnSort.Enabled = true;
        }

        // Enable Search related text box and button
        private void EnableSearchButton()
        {
            txtSearch.Enabled = true;
            btnSearch.Enabled = true;
        }

        // Enable Edit related text box and button
        private void EnableEditButton()
        {
            txtEdit.Enabled = true;
            btnEdit.Enabled = true;
        }
    }
}