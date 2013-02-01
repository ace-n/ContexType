#Region "Tries"
' Tries implementation [8], [9]
Public Class Trie

    ' Given word and count lists, create a set of appropriate tries
    Shared Function CreateTrie(ByVal RecsListSorted As List(Of Recommendation), ByVal TrieLength As Integer) As NamedCountedList

        ' Set up lists programmatically so they are of the proper depth
        Dim Trie As New NamedCountedList("", New List(Of Object))

        ' Create tries by parsing through the text
        '   Each intermediate trie contains its respective letter
        '   Each terminating trie contains the rest of the word along with the word's count

        ' Prep the last word tried recording list
        Dim LastSplitWord As New List(Of String)
        For i = 0 To TrieLength
            LastSplitWord.Add("")
        Next

        For i = 0 To RecsListSorted.Count - 1

            ' Get current word
            Dim CurWord As String = RecsListSorted.Item(i).Text

            ' Skip if current word is nil
            If String.IsNullOrWhiteSpace(CurWord) Then
                Continue For
            End If

            ' Split current word up into trie format
            Dim SplitWord As New List(Of String)
            For j = 0 To TrieLength - 1

                ' If current word is shorter than full trie distance, exit the splitting loop
                If j > CurWord.Length - 1 Then
                    Exit For
                End If

                ' Split the word further
                If j <> TrieLength - 1 Then
                    SplitWord.Add(CurWord.Chars(j))
                Else
                    SplitWord.Add(CurWord.Substring(j))
                End If

            Next

            ' Determine where to place splitted word in master trie list
            Dim StartingList As NamedCountedList = Trie
            For j = 0 To Math.Min(TrieLength, CurWord.Length) - 1

                ' Check equality
                If SplitWord.Item(j) <> LastSplitWord.Item(j) Then

                    ' The two lists aren't equal - add the current word into the trie (in its entirety)

                    ' --- Create trie addition ---
                    Dim TrieAddition As NamedCountedList = ListGen_Generate(SplitWord.Count - j)
                    Dim TrieAddRecursor As NamedCountedList
                    TrieAddRecursor = TrieAddition

                    ' Intermediate layers of trie addition
                    For k = j To SplitWord.Count - 2
                        TrieAddRecursor.Name = SplitWord.Item(k)
                        TrieAddRecursor = TrieAddRecursor.List.Item(0)
                    Next

                    ' Final layer of trie addition
                    TrieAddRecursor.Name = SplitWord.Item(SplitWord.Count - 1)
                    TrieAddRecursor.Count = RecsListSorted.Item(i).Number

                    ' --- Add trie addition to trie ---
                    StartingList.List.Add(TrieAddition)

                    ' --- Update last split word ---
                    LastSplitWord.Clear()
                    LastSplitWord.AddRange(SplitWord)

                    ' Make sure last split word list is of the proper minimum length (to prevent errors)
                    While LastSplitWord.Count < TrieLength
                        LastSplitWord.Add("")
                    End While

                    ' Continue with next word
                    Exit For

                Else
                    ' The two lists are equal - recurse into the next list
                    StartingList = StartingList.List.Item(StartingList.List.Count - 1)
                End If

            Next

        Next

        ' Return final Trie
        Return Trie

    End Function

    ' Recursive dynamic depth list generator - master function
    Shared Function ListGen_Generate(ByVal ListDepth As Integer) As NamedCountedList

        ' Define main list
        Dim List As New NamedCountedList("", New List(Of Object))

        ' Recurse
        ListGen_Recurse(List, ListDepth - 1)

        ' Return the final list
        Return List

    End Function

    ' Recursive dynamic depth list generator - recursor
    Shared Sub ListGen_Recurse(ByRef L2 As NamedCountedList, ByRef ListDepth As Integer)

        If ListDepth = 0 Then
            Return
        Else

            ' Add
            L2.List.Add(New NamedCountedList("", New List(Of Object)))

            ' Recurse
            ListGen_Recurse(L2.List.Item(0), ListDepth - 1)

        End If
    End Sub

    ' Recursively search a trie and return all its members that satisfy a given condition
    Shared Function SearchTrie(ByVal Trie As NamedCountedList, ByVal Needle As String, ByVal MinCnt As Integer, ByVal MinLength As Integer, ByVal IgnoreCase As Boolean) As List(Of Recommendation)

        ' List of search matches in the trie
        Dim Matches As New List(Of Recommendation)

        ' Conduct recursive search
        Try
            SearchTrie_Recursor(Trie, Needle, "", 0, Matches, MinCnt, MinLength, IgnoreCase)
        Catch
        End Try

        ' Return matches
        Return Matches

    End Function

    ' Trie search recursor
    Shared Sub SearchTrie_Recursor(ByRef CurTrie As NamedCountedList, ByVal Needle As String, ByVal CurWord As String, ByVal TrieLayer As Integer, ByRef Matches As List(Of Recommendation), ByVal MinCnt As Integer, ByVal MinLength As Integer, ByVal IgnoreCase As Boolean)

        ' If current base trie layer and needle aren

        ' STEPS: recurse(givenTrie as ?)
        '1: Add any tries (in givenTrie) that terminate to a list
        '2: Find valid subtries (before recursing?)
        '3: Recurse into all valid subtries

        ' If the current trie layer is final and matches the needle, add it to the matches list
        '   Ifs are nested for efficiency (to optimize processing power use)
        If CurTrie.Count >= MinCnt AndAlso CurWord.Length >= MinLength AndAlso (CurWord.Length > Needle.Length) AndAlso ( _
            (IgnoreCase AndAlso CurWord.ToLower.StartsWith(Needle.ToLower)) OrElse _
            (CurWord.StartsWith(Needle))) _
        Then

            Try

                ' Add the recommendation to the matches list
                If Form1.O_EntireWord Then
                    Matches.Add(New Recommendation(CurWord, CurTrie.Count))
                Else
                    Matches.Add(New Recommendation(CurWord.Remove(0, Needle.Length), CurTrie.Count))
                End If

            Catch
            End Try

            Return

        End If

        ' -- Variables used in the recursion-calling loop --
        ' Get current needle character
        'Dim CurNeedleChar As Char = Needle.Chars(TrieLayer)
        'If IgnoreCase Then
        '    CurNeedleChar = Char.ToLowerInvariant(CurNeedleChar)
        'End If

        ' Get next trie layer

        ' Current needle character
        Dim CurNeedleChar As String = ""
        If TrieLayer < Needle.Length Then
            If IgnoreCase Then
                CurNeedleChar = Char.ToLowerInvariant(Needle.Chars(TrieLayer))
            Else
                CurNeedleChar = Needle.Chars(TrieLayer)
            End If
        End If

        ' -- Recurse into any matching trie layers --
        For i = 0 To CurTrie.List.Count - 1

            Dim CurTrieLayer As NamedCountedList = CurTrie.List.Item(i)
            Try

                ' Name of next trie to be recursively parsed
                Dim CurTrieName As String = CurTrieLayer.Name
                Dim NextName As String = ""
                If CurTrie.Count = 0 OrElse CurTrie.List.Count <> 0 Then
                    NextName = CurTrieLayer.Name
                End If

                ' Complete recursion
                If TrieLayer < Needle.Length AndAlso CurTrieName.Length > 0 Then
                    If IgnoreCase AndAlso Char.ToLowerInvariant(CurTrieName.Chars(0)) = CurNeedleChar Then
                        SearchTrie_Recursor(CurTrieLayer, Needle, CurWord & NextName, TrieLayer + 1, Matches, MinCnt, MinLength, IgnoreCase)
                    ElseIf CurTrieName.Chars(0) = CurNeedleChar Then
                        SearchTrie_Recursor(CurTrieLayer, Needle, CurWord & NextName, TrieLayer + 1, Matches, MinCnt, MinLength, IgnoreCase)
                    End If
                Else
                    SearchTrie_Recursor(CurTrie.List.Item(i), Needle, CurWord & NextName, TrieLayer + 1, Matches, MinCnt, MinLength, IgnoreCase)
                End If

            Catch
            End Try

        Next

        ' Return
        Return

    End Sub


End Class

' Used in tries
Public Class NamedCountedList

    Dim StrName As String
    Dim LstList As List(Of Object)
    Dim CountInt As Integer = 0

    Public Sub New(ByVal Name As String, ByVal List As List(Of Object))

        StrName = Name
        LstList = List

    End Sub

    Public Property Name As String
        Set(ByVal value As String)
            StrName = value
        End Set
        Get
            Return StrName
        End Get
    End Property

    Public Property List As List(Of Object)
        Set(ByVal value As List(Of Object))
            LstList = value
        End Set
        Get
            Return LstList
        End Get
    End Property

    Public Property Count As Integer
        Set(ByVal value As Integer)
            CountInt = value
        End Set
        Get
            Return CountInt
        End Get
    End Property

End Class
#End Region