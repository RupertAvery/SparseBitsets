
# Survey Data Analysis 

Surveys consist of questions, and each question will have two or more choices.  In a standard survey, there are usually two types of questions: demographic questions and opinion questions.  Demographic questions are used to group a set of respondents, while opinion questions are used to determine the sentiment of a group with respect to some data point that some organization wishes to gather.

An engagement survey in a hypothetical company might have the following questions:

**Demographic Questions:**

1. What is your role within the organization?
2. How many years have you been with the company?
3. Which department or team do you work in?
4. What is your age range?
5. What is your highest level of education?

**Opinion Questions:**

1. On a scale of 1 to 10, how satisfied are you with your current work-life balance?
2. How would you rate the effectiveness of communication within the team/department?
3. To what extent do you feel recognized and appreciated for your contributions at work?
4. How comfortable do you feel expressing your opinions and ideas in team meetings?
5. Do you believe there are enough opportunities for professional development and growth within the organization?

Demographic quesions will have varying number of choices. For example, Demographic question 1 might have hundreds of choices depending on the organization size and structure. Department might have up to a thousand. Age range might be a handful or up to 10.

For opinion questions, #1 would have 10 choices, while the others would usually have 5 (I strongly disagree, I disagree, neutral, I agree, I strongly agree).

When processing a survey, it is important to be able to group respondents by their demographics, and calculate the population of each group, and the distribution of that group among the choices of one or more opinion questions.

For example, your data might show that 80% of respondents in the age range 20-30 don't feel recognized and appreciated for their contributions at work. Furthermore, it shows that 60% of those respondents are in Department B.  This information, correlated with other questions that also show similar demographics, might point to a management problem in Department B.  With this knowledge, the company can act upon it, and improves management, increasing employee engagement and productivity, lessening leaves. Everyone is happy. Surveys are big business.

# Describing the Query

How can we model the information above as a query into our survey? First of all we need a way to describe groups, based on the questions and choices. The respondents who have made a choice belong to a group.

Say the age range 20-30 is choice 1 on the demographic question (or coding question) 4.

We can say that the group that represents this choice (aka, the group in this demographic) is

```
CQ4:1
```

Now, the question *To what extent do you feel recognized and appreciated for your contributions at work?*  is opinion question 3, and the choices related to negative sentiments are:

1. I strongly disagree  
2. I disagree. 

or

```
OP3:1 
OP3:2
```

These are two distinct groups of people. We want to capture both strongly negative and negative choices as one group, so we OR them.

```
OP3:1 OR OP3:2
```

For notations sake, since the choices belong to a question and are in a range, we can shorten this to:

```
OP3:1,2 
```

or

```
OP3:1-2 
```

The dash notation represents a range, which can be useful to represent several consecutive choices grouped together.

We now have two groups: Those in the age range 20-30 and those who don't feel recognized and appreciated for their contributions at work.

The intersection of this group is what we are interested in, and we can represent this with the notation AND. 

We can then say that the respondents in the age range 20-30 who don't feel recognized and appreciated for their contributions at work are:

```
CQ4:1 AND OP3:1-2 
```

The N-Size of this group is the population or count of responses matching the query.

If the group belonging to department B has the coding `CQ3:2`, then:

The respondents in the age range 20-30 in department B who don't feel recognized and appreciated for their contributions at work are:

```
CQ4:1 AND CQ3:2 AND OP3:1-2 
```

In a survey analysis, you would be computing these groups across several demographic questions, chosen by the user, across several option question, in a manner similar to this:

**Department B, Age 20-30**

| Opinion Question |  Negative  | Neutral | Positive |
|------------------|------------|---------|----------|
| On a scale of 1 to 10, how satisfied are you with your current work-life balance? | 80% | 5% | 15%
| How would you rate the effectiveness of communication within the team/department? | 90% | 5% | 5% 
| To what extent do you feel recognized and appreciated for your contributions at work? | 60% | 10% | 30% 
| How comfortable do you feel expressing your opinions and ideas in team meetings? | 70% | 4% | 26%
| Do you believe there are enough opportunities for professional development and  growth within the organization? | 40% | 18% | 52%

To build this chart, you need to compute the following groups:

```
CQ4:1 AND CQ3:2 AND OP1:1-3
CQ4:1 AND CQ3:2 AND OP1:4-6
CQ4:1 AND CQ3:2 AND OP1:7-10

CQ4:1 AND CQ3:2 AND OP2:1-2 
CQ4:1 AND CQ3:2 AND OP2:3 
CQ4:1 AND CQ3:2 AND OP2:4-5 

CQ4:1 AND CQ3:2 AND OP3:1-2 
CQ4:1 AND CQ3:2 AND OP3:3 
CQ4:1 AND CQ3:2 AND OP3:4-5

CQ4:1 AND CQ3:2 AND OP4:1-2 
CQ4:1 AND CQ3:2 AND OP4:3 
CQ4:1 AND CQ3:2 AND OP4:4-5

CQ4:1 AND CQ3:2 AND OP5:1-2 
CQ4:1 AND CQ3:2 AND OP5:3 
CQ4:1 AND CQ3:2 AND OP5:4-5
```

This is just one slice (`CQ4:1 AND CQ3:2`). The user might want to view `CQ4:1` only, or another department, or other, more complex combinations, including combinations with other opinion questions that might be able to show additional information to help further understand the state of engagement in your company.

This data could be presented as pie charts, bar charts, stacked bar charts, any way that helps visualize how respondents are doing on the survey.

The actual math involved is mostly getting the population count, and dividing one population count from another larger one.

# The Relational Database Approach

Modelling the survey as a relational database seems intuitive and simple enough:

```sql
Survey
   Id

Question
   Id
   SurveyId
   Text

Choice
   Id
   SurveyId
   QuestionId
   Text

Respondent
   Id
   SurveyId

Response 
   Id
   SurveyId
   RespondentId
   QuestionId
   ChoiceId
```

As an aside, you'll notice that to store one response, we need at least 5 INTs, or 20 bytes.  That's not much of a problem, since disk storage is cheap, but since you load pages into memory to query them, you're still loading those 20 bytes per Response. Of course, indexing these will reduce the need for loading all the data pages into memory.


In order to get the population count of

```
CQ4:1 AND CQ3:2 AND OP1:1-2
```

You would need something like this:

```sql
SELECT COUNT(Id) FROM
(
   SELECT Id FROM Response r
   WHERE SurveyId = 1 and QuestionId = 4 and ChoiceId = 1
   INTERSECT
   SELECT Id FROM Response r
   WHERE SurveyId = 1 and QuestionId = 3 and ChoiceId = 22
   INTERSECT
   (
      SELECT Id FROM Response r
         WHERE SurveyId = 1 and QuestionId = 6 and ChoiceId = 131
      UNION
      SELECT Id FROM Response r
         WHERE SurveyId = 1 and QuestionId = 6 and ChoiceId = 132
   )
) AS Q
```

Of course, this is just one data point in a table or chart.

This is a simple survey query. Actual survey queries can be quite complex, with  nested expressions for the demographics part.

# Response Choices as Bitsets

You should be familiar now with bitsets, where you can think of a bitset as containing an array of unique integers, where each integer represents a bit position in the bitset.

So bitsets are just a list of unique numbers.

Let's go back to the database for a moment, and look at our respondent choices.

In order to identify a respondent's choice (i.e. response) you need the respondent ID and the choice ID. Lets show the Question Id so we know that there are multiple questions being answered.

Instead of Ids, lets use the question and choice numbers, to make it easier to understand: 

| RespondentId |  Question  | Choice |
|--------------|------------|--------|
| 4 |  1  | 1  |
| 5 |  1  | 1  |
| 6 |  1  | 2  |
| 7 |  1  | 2  |
| 8 |  1  | 4  |
| 4 |  2  | 3  |
| 5 |  2  | 2  |
| 6 |  2  | 3  |
| 7 |  2  | 1  |
| 8 |  2  | 1  |

Let's look at Question 1, Choice 1.  There are 2 respondents who selected this choice, so they are our group `CQ1:1`

| RespondentId |  Question  | Choice |
|--------------|------------|--------|
| 4 |  1  | 1  |
| 5 |  1  | 1  |

Grouping respondents by question and choice we get:

|  Group  |  RespondentIds  |
|---------|-----------------|
|  CQ1:1  |   4,5   |
|  CQ1:2  |   6,7   |
|  CQ1:3  |   8     |
|  CQ2:1  |   7,8   |
|  CQ2:2  |   5     |
|  CQ2:3  |   4,6   |

Each group contains a list of unique Ids.  Since bit arrays are just a list of unique ids, where a bit is set to 1 in place of the position of the id, we can convert this to:


|  Group  |  RespondentIds  |
|---------|-----------------|
|  CQ1:1  |  00000000 00110000  |
|  CQ1:2  |  00000000 11000000  |
|  CQ1:3  |  00000001 00000000  |
|  CQ2:1  |  00000001 10000000  |
|  CQ2:2  |  00000000 00100000  |
|  CQ2:3  |  00000000 01010000  |

If we wanted to find `CQ1:1 AND CQ2:2` we perform the bitwise AND on both bitsets which is

```
CQ1:1      00000000 00110000
CQ2:2      00000000 00100000
           00000000 00100000
```

So, the group of people who chose both `CQ1:1` and `CQ2:2` contains only (in this small survey), respondent 4.

The population count is the count of bits that are set to 1, which is done without looping using this wonderful piece of code. 



This is basically what survey analysis is all about. Grouping and and counting! 

# Processing Responses

First, we have to pre-process the responses to build our bitsets.  We end up with bit arrays, which we store on disk with a way to recall a specific bitset given a choice or question.

To perform analysis, we take our survey query, and for each group term, we load the required bitset, then perform bitwise operators on them.

As an optimization, we can cache frequently used bitset combinations such as in our example `CQ4:1 AND CQ3:2`, since the combination of a bitset is also a bitset.













In a survey, you will have a unique set of IDs representing each respondent. A survey consists of questions, each question has 2 or more choices. Each choice will have a corresponding bitset created for it. So a 3-choice question will have 3 bitsets. (e.g. Yes, No, Undecided).  

Each bitset then represents the group of people that selected that choice. Conceptually, when a user selects a choice, the user is added to the group of respondents selected that choice. 

In terms of bitsets, we add the userId to the bitset.

```cs
      var bitset = LoadChoiceBitset(choiceId);

      bitset.Add(userId);

      SaveChoiceBitset(choiceId, bitset);
```

In this scenario, a respondent can only have one choice as an answer to the question.  It's also possible that a question can have multiple choices. In a real survey, you can have sub-questions as well (1a, 1b). Regardless, each choice for each question simply needs to be modeled as a bitset.  The grouping of each choice to a question is a simply a business rule.

In a survey, it is often required to "slice" the data into more refined groups of people in order to gain insight into the data.  For example, you might want to find out how many males in Department A play basketball.

To do this, you would load the bitset for Gender:Male, the bitset for Department:A, and the bitset for Sports:Basketball, then `And()` them together. You then call `GetPopCount()` to get the number of people in the resulting group.  You could also return the list of ids in the resulting group by calling `GetValues()`.

```cs
      var males = LoadChoiceBitset(maleChoiceId);
      var deptA = LoadChoiceBitset(deptAChoiceId);
      var basketball = LoadChoiceBitset(basketballChoiceId);

      var result = males.And(deptA).And(basketball);

      var total = result.GetPopCount();
```

Or in a loop:

```cs
      var choiceIds = new List<int> { deptAChoiceId, basketballChoiceId };

      // Since we're ANDing, we need to load the initial bitset first
      var bitset = LoadChoiceBitset(maleChoiceId);
         
      foreach(var choiceId in choiceIds) {
           var temp = LoadChoiceBitset(choiceId);
           bitset = bitset.And(temp);
      }

      var total = result.GetPopCount();
```


### Choice Ranges

If you have a question whose choices are numeric, and you need to get a group representing a range of values, you need to `Or()` the range of bitsets.  So for example, if there is a question that lets people choose a range of 1-10, and you want to find out how many people chose a value of 5 and above, you would need to:

```cs
      var choice5 = LoadChoiceBitset(choice5Id);
      var choice6 = LoadChoiceBitset(choice6Id);
      var choice7 = LoadChoiceBitset(choice7Id);
      var choice8 = LoadChoiceBitset(choice8Id);
      var choice9 = LoadChoiceBitset(choice9Id);
      var choice10 = LoadChoiceBitset(choice10Id);

      var result = choice5.Or(choice6)
                      .Or(choice7)
                      .Or(choice8)
                      .Or(choice9)
                      .Or(choice10);

      var total = result.GetPopCount();
```

Of course, everything could be done in a loop.

```cs
      var bitset = new SparseBitset();

      foreach(var choiceId in choiceIds) {
           var choice = LoadChoiceBitset(choiceId);
           bitset = bitset.Or(choice7);
      }

      var total = result.GetPopCount();
```


In a real-world application, you might want to process a bunch of responses and process choices one batch of users at a time and avoid loading and unloading bitsets unnecessarily.  You could also cache all or some of the bitsets in memory, process them, then persist them in a batch.


